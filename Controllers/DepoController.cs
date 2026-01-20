using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using WorkFlowApp.Data;
using WorkFlowApp.Models;
using System.Threading.Tasks;

namespace WorkFlowApp.Controllers
{
    
    [Authorize(Roles = "Admin,GenelMudur,Depo,Tekstil,Kaplama")]
    public class DepoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepoController(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool IsReadOnlyUser()
        {
            return User.IsInRole("GenelMudur") || User.IsInRole("Tekstil") || User.IsInRole("Kaplama");
        }
        public IActionResult Index() => RedirectToAction("TekstilPamuklu");
        public IActionResult TekstilPamuklu(DateTime? tarih)
        {
            _context.Database.EnsureCreated();
            var secilenTarih = tarih.HasValue ? tarih.Value.Date : DateTime.Today;
            ViewBag.SecilenTarih = secilenTarih;

            var liste = _context.TekstilStoklari
                                .Where(x => x.Tur == "Pamuklu" && x.GirisTarihi.Date == secilenTarih)
                                .OrderBy(x => x.Kategori)
                                .ThenBy(x => x.AltKategori)
                                .ToList();
            return View(liste);
        }

        public IActionResult TekstilSentetik(DateTime? tarih)
        {
            _context.Database.EnsureCreated();
            var secilenTarih = tarih.HasValue ? tarih.Value.Date : DateTime.Today;
            ViewBag.SecilenTarih = secilenTarih;

            var liste = _context.TekstilStoklari
                                .Where(x => x.Tur == "Sentetik" && x.GirisTarihi.Date == secilenTarih)
                                .OrderBy(x => x.Kategori)
                                .ThenBy(x => x.AltKategori)
                                .ToList();
            return View(liste);
        }

        [HttpPost]
        public IActionResult StokGuncelle(int id, int yeniCuvalSayisi)
        {
            if (IsReadOnlyUser()) return Forbid(); 

            var urun = _context.TekstilStoklari.Find(id);
            if (urun != null && urun.CuvalIciAdet > 0)
            {
                urun.CuvalSayisi = yeniCuvalSayisi;
                urun.Miktar = yeniCuvalSayisi * urun.CuvalIciAdet;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult StokMiktarGuncelle(int id, double yeniMiktar)
        {
            if (IsReadOnlyUser()) return Forbid();

            var urun = _context.TekstilStoklari.Find(id);
            if (urun != null)
            {
                urun.Miktar = yeniMiktar;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult TekstilExcelYukle(IFormFile excelDosyasi, DateTime? tarihSecimi)
        {
            if (IsReadOnlyUser()) return Forbid();
            if (excelDosyasi == null || excelDosyasi.Length == 0) return RedirectToAction("TekstilPamuklu");

            var islemTarihi = tarihSecimi.HasValue ? tarihSecimi.Value.Date : DateTime.Today;

            var bugununVerileri = _context.TekstilStoklari.Where(x => x.GirisTarihi.Date == islemTarihi).ToList();
            _context.TekstilStoklari.RemoveRange(bugununVerileri);
            _context.SaveChanges();

            ExcelPackage.License.SetNonCommercialPersonal("Bedirhan");

            using (var stream = new MemoryStream())
            {
                excelDosyasi.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    if (package.Workbook.Worksheets.Count > 0) IslePamukluSayfasi(package.Workbook.Worksheets[0], islemTarihi);
                    if (package.Workbook.Worksheets.Count > 1) IsleBrodeSayfasi(package.Workbook.Worksheets[1], islemTarihi);
                    if (package.Workbook.Worksheets.Count > 2) IsleSentetikSayfasi(package.Workbook.Worksheets[2], islemTarihi);
                    if (package.Workbook.Worksheets.Count > 3) IsleSentetikIplikSayfasi(package.Workbook.Worksheets[3], islemTarihi);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("TekstilPamuklu", new { tarih = islemTarihi });
        }

        public IActionResult Paketleme(DateTime? tarih)
        {
            _context.Database.EnsureCreated();
            var islemTarihi = tarih.HasValue ? tarih.Value.Date : DateTime.Today;
            ViewBag.SecilenTarih = islemTarihi;

            var bugununKayitlari = _context.PaketlemeStoklari.Where(x => x.Tarih.Date == islemTarihi).ToList();

            if (!bugununKayitlari.Any())
            {
                var tumUrunler = _context.PaketlemeStoklari.Select(x => new { x.UrunAdi, x.Tur }).Distinct().ToList();

                foreach (var urun in tumUrunler)
                {
                    var sonKayit = _context.PaketlemeStoklari
                                           .Where(x => x.UrunAdi == urun.UrunAdi && x.Tarih < islemTarihi)
                                           .OrderByDescending(x => x.Tarih)
                                           .FirstOrDefault();

                    int devirSayisi = sonKayit != null ? sonKayit.Kalan : 0;

                    _context.PaketlemeStoklari.Add(new PaketlemeStok
                    {
                        UrunAdi = urun.UrunAdi,
                        Tur = urun.Tur,
                        Tarih = islemTarihi,
                        Devir = devirSayisi,
                        Giris = 0, Iade = 0, Cikis = 0, Kalan = devirSayisi
                    });
                }
                _context.SaveChanges();
                bugununKayitlari = _context.PaketlemeStoklari.Where(x => x.Tarih.Date == islemTarihi).ToList();
            }

            return View(bugununKayitlari.OrderBy(x => x.UrunAdi).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> PaketDosyaYukle(int id, IFormFile dosya)
        {
            if (IsReadOnlyUser()) return Forbid(); 

            var stok = _context.PaketlemeStoklari.Find(id);
            if (stok == null || dosya == null || dosya.Length == 0) return BadRequest();

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dosya.CopyToAsync(stream);
            }

            stok.DosyaYolu = "/uploads/" + fileName; 
            _context.SaveChanges();

            return RedirectToAction("Paketleme", new { tarih = stok.Tarih });
        }

        [HttpPost]
        public IActionResult PaketlemeGuncelle(int id, string alan, int deger)
        {
            if (IsReadOnlyUser()) return Forbid();
            var stok = _context.PaketlemeStoklari.Find(id);
            if (stok != null)
            {
                if (alan == "Giris") stok.Giris = deger;
                else if (alan == "Iade") stok.Iade = deger;
                else if (alan == "Cikis") stok.Cikis = deger;

                stok.Kalan = stok.Devir + stok.Giris + stok.Iade - stok.Cikis;
                _context.SaveChanges();
                return Ok(stok.Kalan);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult PaketlemeExcelYukle(IFormFile excelDosyasi, DateTime? tarihSecimi)
        {
            if (IsReadOnlyUser()) return Forbid();
            if (excelDosyasi == null || excelDosyasi.Length == 0) return RedirectToAction("Paketleme");
            var islemTarihi = tarihSecimi.HasValue ? tarihSecimi.Value.Date : DateTime.Today;

            var bugununVerileri = _context.PaketlemeStoklari.Where(x => x.Tarih.Date == islemTarihi).ToList();
            _context.PaketlemeStoklari.RemoveRange(bugununVerileri);
            _context.SaveChanges();

            ExcelPackage.License.SetNonCommercialPersonal("Bedirhan");
            using (var stream = new MemoryStream())
            {
                excelDosyasi.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    if (package.Workbook.Worksheets.Count > 0) IslePaketlemeSayfasi(package.Workbook.Worksheets[0], "Pamuklu", islemTarihi);
                    if (package.Workbook.Worksheets.Count > 1) IslePaketlemeSayfasi(package.Workbook.Worksheets[1], "Sentetik", islemTarihi);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Paketleme", new { tarih = islemTarihi });
        }

        public IActionResult Mamul(DateTime? tarih)
        {
            _context.Database.EnsureCreated();
            var islemTarihi = tarih.HasValue ? tarih.Value.Date : DateTime.Today;
            ViewBag.SecilenTarih = islemTarihi;

            var bugununKayitlari = _context.MamulStoklari.Where(x => x.Tarih.Date == islemTarihi).ToList();

            if (!bugununKayitlari.Any())
            {
                var tumUrunler = _context.MamulStoklari.Select(x => x.UrunAdi).Distinct().ToList();

                foreach (var urunAdi in tumUrunler)
                {
                    var sonKayit = _context.MamulStoklari
                                           .Where(x => x.UrunAdi == urunAdi && x.Tarih < islemTarihi)
                                           .OrderByDescending(x => x.Tarih)
                                           .FirstOrDefault();

                    int devirSayisi = sonKayit != null ? sonKayit.Kalan : 0;

                    _context.MamulStoklari.Add(new MamulStok
                    {
                        UrunAdi = urunAdi,
                        Tarih = islemTarihi,
                        Devir = devirSayisi,
                        Uretim = 0, Sevk = 0, Kalan = devirSayisi
                    });
                }
                _context.SaveChanges();
                bugununKayitlari = _context.MamulStoklari.Where(x => x.Tarih.Date == islemTarihi).ToList();
            }

            return View(bugununKayitlari.OrderBy(x => x.UrunAdi).ToList());
        }

        [HttpPost]
        public IActionResult MamulGuncelle(int id, string alan, int deger)
        {
            if (IsReadOnlyUser()) return Forbid();
            var stok = _context.MamulStoklari.Find(id);
            if (stok != null)
            {
                if (alan == "Uretim") stok.Uretim = deger;
                else if (alan == "Sevk") stok.Sevk = deger;

                stok.Kalan = stok.Devir + stok.Uretim - stok.Sevk;
                _context.SaveChanges();
                return Ok(stok.Kalan);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult MamulExcelYukle(IFormFile excelDosyasi, DateTime? tarihSecimi)
        {
            if (IsReadOnlyUser()) return Forbid();
            if (excelDosyasi == null || excelDosyasi.Length == 0) return RedirectToAction("Mamul");
            var islemTarihi = tarihSecimi.HasValue ? tarihSecimi.Value.Date : DateTime.Today;

            var bugununVerileri = _context.MamulStoklari.Where(x => x.Tarih.Date == islemTarihi).ToList();
            _context.MamulStoklari.RemoveRange(bugununVerileri);
            _context.SaveChanges();

            ExcelPackage.License.SetNonCommercialPersonal("Bedirhan");
            using (var stream = new MemoryStream())
            {
                excelDosyasi.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    foreach (var sheet in package.Workbook.Worksheets)
                    {
                        IsleMamulSayfasi(sheet, islemTarihi);
                    }
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Mamul", new { tarih = islemTarihi });
        }

        public IActionResult Iplik()
        {
            _context.Database.EnsureCreated();
            var iplikler = _context.IplikStoklari
                                   .Where(x => x.KalanKg > 0)
                                   .OrderByDescending(x => x.GelisTarihi)
                                   .ToList();
            return View(iplikler);
        }

        [HttpPost]
        public IActionResult IplikEkle(string firma, string urun, string lot, double kilo, DateTime tarih)
        {
            if (IsReadOnlyUser()) return Forbid();
            var yeniIplik = new IplikStok
            {
                Firma = firma,
                UrunAdi = urun,
                LotNo = lot,
                GelenKg = kilo,
                KalanKg = kilo,
                GelisTarihi = tarih
            };
            
            _context.IplikStoklari.Add(yeniIplik);
            _context.SaveChanges();
            return RedirectToAction("Iplik");
        }

        [HttpPost]
        public IActionResult IplikDus(int id, double dusulenMiktar)
        {
            if (IsReadOnlyUser()) return Forbid();
            var iplik = _context.IplikStoklari.Find(id);
            if (iplik != null)
            {
                if(iplik.KalanKg >= dusulenMiktar)
                {
                    iplik.KalanKg -= dusulenMiktar;
                    _context.SaveChanges();
                    return Ok("Başarılı");
                }
                return BadRequest("Yetersiz Stok!");
            }
            return BadRequest("Kayıt bulunamadı!");
        }

        private void IslePamukluSayfasi(ExcelWorksheet sheet, DateTime tarih)
        {
            int rowCount = sheet.Dimension?.Rows ?? 0;
            string currentSubCategory = "Kesilmiş";
            string tamirDurumu = "";

            for (int row = 2; row <= rowCount; row++)
            {
                var colA = sheet.Cells[row, 1].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(colA))
                {
                    if (colA.Contains("Overloklu", StringComparison.OrdinalIgnoreCase)) currentSubCategory = "Overloklu";
                    else if (colA.Contains("Çevrilmiş", StringComparison.OrdinalIgnoreCase)) currentSubCategory = "Çevrilmiş";
                    else if (colA.Contains("Kesilmiş", StringComparison.OrdinalIgnoreCase)) currentSubCategory = "Kesilmiş";
                    else if (!colA.Contains("Toplam", StringComparison.OrdinalIgnoreCase))
                    {
                        var colB = sheet.Cells[row, 2].Value;
                        var colC = sheet.Cells[row, 3].Value;
                        int cuval = 0; int ici = 0;
                        if (colB != null) int.TryParse(colB.ToString(), out cuval);
                        if (colC != null) int.TryParse(colC.ToString(), out ici);
                        double toplam = cuval * ici;

                        EkleStok(colA, toplam, "Çift", "Pamuklu", "Urun", currentSubCategory, tarih, cuval, ici);
                    }
                }
                var colG = sheet.Cells[row, 7].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(colG))
                {
                    if (colG.Contains("Yapılacak", StringComparison.OrdinalIgnoreCase)) tamirDurumu = "Yapılacak Tamir";
                    else if (colG.Contains("Yapılmış", StringComparison.OrdinalIgnoreCase)) tamirDurumu = "Yapılmış Tamir";
                    else
                    {
                        var colH = sheet.Cells[row, 8].Value;
                        if (colH != null && double.TryParse(colH.ToString(), out double tamirMiktar))
                            EkleStok(colG, tamirMiktar, "Çift", "Pamuklu", "Tamir", tamirDurumu, tarih);
                    }
                }
            }
        }
        
        private void IsleBrodeSayfasi(ExcelWorksheet sheet, DateTime tarih)
        {
            int rowCount = sheet.Dimension?.Rows ?? 0;
            for (int row = 2; row <= rowCount; row++)
            {
                var colA = sheet.Cells[row, 1].Value?.ToString();
                if (string.IsNullOrWhiteSpace(colA)) continue;
                if (row < 25)
                {
                    var colD = sheet.Cells[row, 4].Value;
                    if (colD != null && double.TryParse(colD.ToString(), out double miktar))
                        EkleStok(colA, miktar, "Çift", "Pamuklu", "Brode", "Brode", tarih);
                }
                else
                {
                    var colD = sheet.Cells[row, 4].Value;
                    var colE = sheet.Cells[row, 5].Value?.ToString() ?? "Adet";
                    if (colD != null && double.TryParse(colD.ToString(), out double miktar))
                        EkleStok(colA, miktar, colE, "Pamuklu", "Iplik", "Pamuk İpliği", tarih);
                }
            }
        }

        private void IsleSentetikSayfasi(ExcelWorksheet sheet, DateTime tarih)
        {
            int rowCount = sheet.Dimension?.Rows ?? 0;
            for (int row = 2; row <= rowCount; row++)
            {
                var colA = sheet.Cells[row, 1].Value?.ToString();
                var colE = sheet.Cells[row, 5].Value;
                if (string.IsNullOrWhiteSpace(colA)) continue;
                if (colE != null && double.TryParse(colE.ToString(), out double miktar))
                    EkleStok(colA, miktar, "Çift", "Sentetik", "Urun", "Sentetik Eldiven", tarih);
            }
        }

        private void IsleSentetikIplikSayfasi(ExcelWorksheet sheet, DateTime tarih)
        {
            int rowCount = sheet.Dimension?.Rows ?? 0;
            string currentSubCat = "Polyester Uç";
            for (int row = 2; row <= rowCount; row++)
            {
                var colA = sheet.Cells[row, 1].Value?.ToString();
                if (string.IsNullOrWhiteSpace(colA)) continue;
                if (colA.Contains("Uç Lastikleri", StringComparison.OrdinalIgnoreCase)) currentSubCat = "Uç Lastikleri";
                else if (colA.Contains("Kesilmez", StringComparison.OrdinalIgnoreCase)) currentSubCat = "Kesilmez İplik";
                else if (colA.Contains("Naylon", StringComparison.OrdinalIgnoreCase)) currentSubCat = "Naylon İplik";
                else if (colA.Contains("Polyester", StringComparison.OrdinalIgnoreCase)) currentSubCat = "Polyester İplik";
                var colC = sheet.Cells[row, 3].Value;
                if (colC != null && double.TryParse(colC.ToString(), out double miktar))
                    EkleStok(colA, miktar, "KG", "Sentetik", "Iplik", currentSubCat, tarih);
            }
        }

        private void IslePaketlemeSayfasi(ExcelWorksheet sheet, string tur, DateTime tarih)
        {
            int rowCount = sheet.Dimension?.Rows ?? 0;
            for (int row = 2; row <= rowCount; row++)
            {
                var urunAdi = sheet.Cells[row, 1].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(urunAdi) && !urunAdi.Contains("TOPLAM", StringComparison.OrdinalIgnoreCase))
                    _context.PaketlemeStoklari.Add(new PaketlemeStok { UrunAdi = urunAdi, Tur = tur, Tarih = tarih });
            }
        }

        private void IsleMamulSayfasi(ExcelWorksheet sheet, DateTime tarih)
        {
            int rowCount = sheet.Dimension?.Rows ?? 0;
            for (int row = 2; row <= rowCount; row++)
            {
                var urunAdi = sheet.Cells[row, 1].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(urunAdi) && urunAdi.Length > 3 && !urunAdi.Contains("DEVİR"))
                {
                    var mevcutMu = _context.MamulStoklari.Local.Any(x => x.UrunAdi == urunAdi && x.Tarih == tarih);
                    if (!mevcutMu)
                    {
                        _context.MamulStoklari.Add(new MamulStok
                        {
                            UrunAdi = urunAdi,
                            Tarih = tarih,
                            Devir = 0, Uretim = 0, Sevk = 0, Kalan = 0
                        });
                    }
                }
            }
        }

        private void EkleStok(string isim, double miktar, string birim, string tur, string kategori, string altKategori, DateTime tarih, int cuval = 0, int cuvalIci = 0)
        {
            _context.TekstilStoklari.Add(new TekstilStok
            {
                Cinsi = isim,
                Miktar = miktar,
                Birim = birim,
                Tur = tur,
                Kategori = kategori,
                AltKategori = altKategori,
                GirisTarihi = tarih,
                CuvalSayisi = cuval,
                CuvalIciAdet = cuvalIci
            });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 
using WorkFlowApp.Data;
using WorkFlowApp.Models;
using System.Linq;
using System;

namespace WorkFlowApp.Controllers
{
   
    [Authorize(Roles = "Admin,GenelMudur,Pazarlama")]
    public class PazarlamaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PazarlamaController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index()
        {
            _context.Database.EnsureCreated();

            var siparisler = _context.SatisSiparisleri
                                     .OrderByDescending(x => x.SiparisTarihi)
                                     .ToList();

            var model = new PazarlamaViewModel
            {
                Siparisler = siparisler,
                ToplamCiro = siparisler.Sum(x => x.Adet * x.BirimFiyat)
            };

            return View(model);
        }

        
        public IActionResult SiparisGir()
        {
            if (User.IsInRole("GenelMudur")) return Forbid(); 

            ViewBag.Urunler = _context.MamulStoklari.Select(x => x.UrunAdi).Distinct().ToList();
            return View();
        }
        [HttpPost]
        public IActionResult SiparisEkle(string musteri, string urun, int adet, decimal fiyat)
        {
          
            if (User.IsInRole("GenelMudur")) return Forbid();

            var yeniSiparis = new PazarlamaSiparis
            {
                MusteriAdi = musteri,
                UrunAdi = urun,
                Adet = adet,
                BirimFiyat = fiyat,
                SiparisTarihi = DateTime.Now,
                Tamamlandi = false
            };

            _context.SatisSiparisleri.Add(yeniSiparis);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public IActionResult Tamamla(int id)
        {
           
            if (User.IsInRole("GenelMudur")) return Forbid();

            var siparis = _context.SatisSiparisleri.Find(id);
            if(siparis != null)
            {
                siparis.Tamamlandi = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
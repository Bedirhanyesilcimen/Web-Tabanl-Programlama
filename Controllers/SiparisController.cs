using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkFlowApp.Data;
using WorkFlowApp.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace WorkFlowApp.Controllers
{
    [Authorize]
    public class SiparisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiparisController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SiparisVer()
        {
            if (User.IsInRole("GenelMudur"))
            {
                return RedirectToAction("BekleyenSiparisler");
            }
            return View();
        }

        [HttpPost]
        public IActionResult SiparisVer(SatinalmaTalep talep)
        {
            if (User.IsInRole("GenelMudur")) return Unauthorized();

            if (talep != null)
            {
                talep.TalepEden = User.Identity?.Name ?? "Bilinmiyor";
                talep.TalepTarihi = DateTime.Now;
                talep.Tarih = DateTime.Now;
                talep.Durum = "Genel Müdür Onayında";
                
                if (string.IsNullOrEmpty(talep.Aciklama)) talep.Aciklama = "-";
                
                _context.SatinalmaTalepleri.Add(talep);
                _context.SaveChanges();
            }
            return RedirectToAction("BekleyenSiparisler");
        }

        public IActionResult BekleyenSiparisler()
        {
            var userName = User.Identity?.Name;
            bool isYetkili = User.IsInRole("Admin") || User.IsInRole("GenelMudur") || User.IsInRole("Muhasebe") || User.IsInRole("Depo");

            List<SatinalmaTalep> talepler;

            if (isYetkili)
            {
                talepler = _context.SatinalmaTalepleri.OrderByDescending(x => x.TalepTarihi).ToList();
            }
            else
            {
                talepler = _context.SatinalmaTalepleri
                                  .Where(x => x.TalepEden == userName)
                                  .OrderByDescending(x => x.TalepTarihi)
                                  .ToList();
            }

            return View(talepler);
        }

        [HttpPost]
        public IActionResult DurumGuncelle(int id)
        {
            var talep = _context.SatinalmaTalepleri.Find(id);
            if (talep == null) return NotFound();
            if (User.IsInRole("GenelMudur") && talep.Durum == "Genel Müdür Onayında")
                talep.Durum = "Muhasebe Sipariş Geçti";
            else if (User.IsInRole("Muhasebe") && talep.Durum == "Muhasebe Sipariş Geçti")
                talep.Durum = "Ürün Depoya Geldi";
            else if (User.IsInRole("Depo") && talep.Durum == "Ürün Depoya Geldi")
                talep.Durum = "Teslim Edildi";
            else if (User.IsInRole("Admin"))
            {
                if (talep.Durum == "Genel Müdür Onayında") talep.Durum = "Muhasebe Sipariş Geçti";
                else if (talep.Durum == "Muhasebe Sipariş Geçti") talep.Durum = "Ürün Depoya Geldi";
                else if (talep.Durum == "Ürün Depoya Geldi") talep.Durum = "Teslim Edildi";
            }

            _context.SaveChanges();
            return RedirectToAction("BekleyenSiparisler");
        }
    }
}

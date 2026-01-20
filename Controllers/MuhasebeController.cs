using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkFlowApp.Data;
using WorkFlowApp.Models;
using System.Linq;
using System.Collections.Generic;

namespace WorkFlowApp.Controllers
{
    [Authorize(Roles = "Admin,Muhasebe,GenelMudur")] 
    public class MuhasebeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MuhasebeController(ApplicationDbContext context) => _context = context;

        public IActionResult Index()
        {
            var talepler = _context.SatinalmaTalepleri.OrderByDescending(x => x.Id).ToList();
            ViewBag.MuhasebeListesi = _context.MuhasebeKayitlari.OrderByDescending(x => x.Id).ToList();

            return View(talepler);
        }

        [HttpPost]
        public IActionResult Kaydet(MuhasebeKayit model)
        {
            if (User.IsInRole("GenelMudur")) return Forbid();

            if (model != null)
            {
                _context.MuhasebeKayitlari.Add(model);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Sil(int id)
        {
            if (User.IsInRole("GenelMudur")) return Forbid();

            var kayit = _context.MuhasebeKayitlari.Find(id);
            if (kayit != null)
            {
                _context.MuhasebeKayitlari.Remove(kayit);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
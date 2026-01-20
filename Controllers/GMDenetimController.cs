using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkFlowApp.Data;
using WorkFlowApp.Models;
using System.Linq;
using System;

namespace WorkFlowApp.Controllers
{
    [Authorize(Roles = "Admin,GenelMudur")]
    public class GMDenetimController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GMDenetimController(ApplicationDbContext context) => _context = context;

        public IActionResult Index()
        {
            ViewBag.ToplamPersonel = _context.AppUsers.Count();
            ViewBag.BekleyenSiparisler = _context.SatinalmaTalepleri.Count(x => x.Durum != "Teslim Edildi");
            return View();
        }

        [HttpGet]
        public IActionResult RaporTalepEt() => View();

        [HttpPost]
        public IActionResult RaporTalepEt(RaporTalebi model)
        {
            model.OlusturulmaTarihi = DateTime.Now;
            model.Durum = "Bekliyor";
            model.TalepEden = User.Identity.Name ?? "Genel Müdür";

            if (ModelState.IsValid)
            {
                _context.RaporTalepleri.Add(model);
                _context.SaveChanges();
                return RedirectToAction("RaporTakip");
            }
            return View(model);
        }

        public IActionResult RaporTakip()
        {
            var taleptakip = _context.RaporTalepleri.OrderBy(x => x.SonTarih).ToList();
            return View(taleptakip);
        }
    }
}
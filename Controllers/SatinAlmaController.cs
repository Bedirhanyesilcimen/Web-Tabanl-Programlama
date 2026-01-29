using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Data;
using WorkFlowApp.Models;
using System.Linq;

namespace WorkFlowApp.Controllers
{
    public class SatinAlmaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SatinAlmaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var liste = _context.SatinalmaTalepleri.OrderByDescending(x => x.TalepTarihi).ToList();
            return View(liste);
        }

        [HttpPost]
        public IActionResult TalepOlustur(SatinalmaTalep model)
        {
            model.TalepTarihi = DateTime.Now;
            model.Tarih = DateTime.Now; 
            model.Durum = "Genel Müdür Onayında";
            
            _context.SatinalmaTalepleri.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

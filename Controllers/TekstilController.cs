using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkFlowApp.Data;
using WorkFlowApp.Models;
using System;
using System.Linq;

namespace WorkFlowApp.Controllers
{
    [Authorize(Roles = "Admin,Tekstil,GenelMudur")]
    public class TekstilController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TekstilController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index()
        {
            var viewModel = new TekstilViewModel
            {
                Makineler = _context.Makineler.ToList(),
                Uretimler = _context.TekstilUretimleri.OrderByDescending(x => x.Tarih).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UretimGir(TekstilUretim yeniUretim)
        {
            if (User.IsInRole("GenelMudur")) return Forbid();
            
            yeniUretim.Tarih = DateTime.Now;

            if (ModelState.IsValid || yeniUretim.Miktar > 0)
            {
                _context.TekstilUretimleri.Add(yeniUretim);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}
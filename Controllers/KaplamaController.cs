using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkFlowApp.Data;
using WorkFlowApp.Models;
using System.Linq;

namespace WorkFlowApp.Controllers
{
    [Authorize]
    public class KaplamaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public KaplamaController(ApplicationDbContext context) => _context = context;

        public IActionResult Index()
        {
            var veriler = _context.KaplamaUretimleri.OrderByDescending(x => x.Tarih).ToList();
            return View(veriler);
        }

        [HttpPost]
        public IActionResult VeriKaydet(KaplamaVeri model) 
        {
            if (User.IsInRole("GenelMudur")) return Unauthorized();

            if (ModelState.IsValid)
            {
                _context.KaplamaUretimleri.Add(model);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
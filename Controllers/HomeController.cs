using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Data;
using WorkFlowApp.Models;

namespace WorkFlowApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            _context.Database.EnsureCreated();

            double tekstil = _context.TekstilStoklari.Any() ? _context.TekstilStoklari.Sum(x => x.Miktar) : 0;
            int paketleme = _context.PaketlemeStoklari.Any() ? _context.PaketlemeStoklari.Sum(x => x.Kalan) : 0;
            int mamul = _context.MamulStoklari.Any() ? _context.MamulStoklari.Sum(x => x.Kalan) : 0;
            double iplik = 0;
            try { iplik = _context.IplikStoklari.Any() ? _context.IplikStoklari.Sum(x => x.KalanKg) : 0; } catch { }

            ViewBag.TekstilStok = tekstil;
            ViewBag.PaketlemeStok = paketleme;
            ViewBag.MamulStok = mamul;
            ViewBag.IplikStok = iplik;

            return View(new DashboardViewModel());
        }
    }
}
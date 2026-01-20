using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Data;
using WorkFlowApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace WorkFlowApp.Controllers
{
    public class UretimController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UretimController(ApplicationDbContext context) => _context = context;

        public IActionResult Index()
        {
            
            List<KaplamaVeri> liste = _context.KaplamaUretimleri.OrderByDescending(x => x.Tarih).ToList();
            return View(liste);
        }
    }
}
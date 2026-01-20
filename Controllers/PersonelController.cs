using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkFlowApp.Data;
using WorkFlowApp.Models;
using System.Linq;

namespace WorkFlowApp.Controllers
{
    [Authorize(Roles = "Admin,GenelMudur")]
    public class PersonelController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PersonelController(ApplicationDbContext context) => _context = context;

        public IActionResult Index()
        {
            var personeller = _context.AppUsers.ToList();
            return View(personeller);
        }

        [HttpPost]
        public IActionResult Ekle(AppUser model)
        {
            if (User.IsInRole("GenelMudur")) return Forbid();
            
            if (ModelState.IsValid)
            {
                _context.AppUsers.Add(model);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Sil(int id)
        {
            if (User.IsInRole("GenelMudur")) return Forbid(); 
            
            var personel = _context.AppUsers.Find(id);
            if (personel != null)
            {
                _context.AppUsers.Remove(personel);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
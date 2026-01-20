using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Data;
using WorkFlowApp.Models;

namespace WorkFlowApp.Controllers
{
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SeedController(ApplicationDbContext context) { _context = context; }

        [Route("Sifirla")]
        public IActionResult Index()
        {
            // Kullanıcı yoksa ekle
            if (!_context.AppUsers.Any())
            {
                _context.AppUsers.Add(new AppUser { FullName="Yönetici", Username="admin", Password="123", Role="Admin" });
                _context.SaveChanges();
                return Content("✅ Admin Eklendi: admin / 123");
            }
            return Content("Zaten kullanıcı var.");
        }
    }
}
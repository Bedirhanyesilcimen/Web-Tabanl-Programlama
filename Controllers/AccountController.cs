using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Session için mutlaka olmalı
using WorkFlowApp.Data;
using WorkFlowApp.Models;

namespace WorkFlowApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string kadi, string sifre)
        {
            _context.Database.EnsureCreated();

            // Admin kontrolü ve oluşturma
            if (!_context.AppUsers.Any())
            {
                _context.AppUsers.Add(new AppUser { Username = "admin", Password = "1234", Role = "Admin", FullName = "Sistem Yöneticisi" });
                _context.SaveChanges();
            }

            var user = _context.AppUsers.FirstOrDefault(x => x.Username == kadi && x.Password == sifre);

            if (user != null)
            {
                // PERSONEL SAYFASININ İSTEDİĞİ VERİYİ BURADA YAZIYORUZ:
                HttpContext.Session.SetString("Role", user.Role);
                HttpContext.Session.SetString("Username", user.Username);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Hata = "Hatalı Giriş!";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
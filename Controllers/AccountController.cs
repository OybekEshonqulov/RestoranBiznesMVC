using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestoranBoshqaruvi.Models;
using System.Security.Claims;

namespace RestoranBoshqaruvi.Controllers
{
    public class AccountController : Controller
    {
        // Login sahifasini ko'rsatish
        public IActionResult Login()
        {
            return View();
        }

        // Login qilish (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Foydalanuvchi autentifikatsiyasi (masalan, bazada tekshirish)
                var user = AuthenticateUser(model.Username, model.Password);
                if (user != null)
                {
                    // Cookie yaratish
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role) // Foydalanuvchi roli
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }

        // Logout (Foydalanuvchini tizimdan chiqarmoq)
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // Foydalanuvchi autentifikatsiyasini tekshirish (bazaga tekshirish)
        private User AuthenticateUser(string username, string password)
        {
            // Bunda oddiy tekshirish (masalan, qattiq kodlangan foydalanuvchi)
            if (username == "admin" && password == "admin")
            {
                return new User { Username = "admin", Role = "Admin" }; // Login ma'lumotlarini qaytarish
            }
            if (username == "Waiter" && password == "Waiter")
            {
                return new User { Username = "Waiter", Role = "Waiter" }; // Login ma'lumotlarini qaytarish
            }
            if (username == "Chef" && password == "Chef")
            {
                return new User { Username = "Chef", Role = "Chef" }; // Login ma'lumotlarini qaytarish
            }
            return null;
        }
    }
}

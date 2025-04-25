using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DoTechSoftMVC.Models.ViewModels;

namespace DoTechSoftMVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Burada gerçek kullanıcı doğrulamasını yapman gerekir (şimdilik hardcoded örnek):
                if (model.UserName == "admin" && model.Password == "1234")
                {
                    // Kullanıcı doğrulandıysa claim'leri oluştur:
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, "Admin") // Role ekleyebilirsin
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Cookie Authentication ile giriş işlemi:
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe, // Beni hatırla ise kalıcı cookie
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(60) // Cookie süresi
                        });

                    return RedirectToAction("Index", "Home"); // Başarılı giriş sonrası yönlendirme
                }

                // Kullanıcı adı veya şifre yanlışsa:
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            }
            // If we got this far, something failed; redisplay form
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Kullanıcıyı cookie'den çıkış yaptırır:
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Çıkış yaptıktan sonra Login sayfasına yönlendir:
            return RedirectToAction("Login", "Auth");
        }
    }
}

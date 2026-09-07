using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SistemaAPI.Controllers
{
    public class AuthController : Controller
    {
        //mostra a pagina de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //faz envio do form de login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (email == "admin@teste.com" && senha == "123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Email, email)
                };

                var identity = new ClaimsIdentity(claims, "CookieAuth");

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // mostra a pagina de registro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // faz envio do form de registro e redireciona pra login
        [HttpPost]
        public IActionResult Register(string ocnte)
        {
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login", "Auth");
        }
    }
}

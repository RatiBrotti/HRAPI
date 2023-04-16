using HRMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace HRMVC.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Administrator register)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7071/api/")
            };
            var json = JsonSerializer.Serialize(register);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync("/Administrator/CreateAdministrator", content);
                response.EnsureSuccessStatusCode();

                return Content("ki");
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            if (claimuser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if (login.UserName == "raticercvadze@gmail.com" && login.Password == "123")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, login.UserName),
                    new Claim(ClaimTypes.Email, login.Password),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = login.KeepLoggedIn
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return Content("ki");
            }
            return RedirectToAction("Index","Home");
        }
    }
}

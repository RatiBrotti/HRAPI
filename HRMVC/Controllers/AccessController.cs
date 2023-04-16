using HRMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using System.Security.Cryptography;
using ShopMaster_3._0.Tools;

namespace HRMVC.Controllers
{
    public class AccessController : Controller
    {
        public HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
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
            if (register.Password == register.ConfirmPassword && PasswordTools.IsValid(register.Password))
            {
                register.Password = PasswordTools.MD5Hash(register.Password)+ "secret key";
            }
            else
            {
                return BadRequest();
            }
            
            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            //create json
            string json = JsonConvert.SerializeObject(register);

            //create string with json
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //post request
            var response = await client.PostAsync("https://localhost:7071/api/Administrator/CreateAdministrator", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Access");
            }
            else
            {

                return Content("Error creating employee: " + await response.Content.ReadAsStringAsync());

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

            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            // Set the base URL of the API endpoint
            client.BaseAddress = new Uri("https://localhost:7071");

            // Make a GET request to the API endpoint
            //var response = await client.GetAsync("/api/Administrator/Get");

            //// Read the response content as a string
            //var content = await response.Content.ReadAsStringAsync();
            //List<Employee> employee = JsonConvert.DeserializeObject<List<Employee>>(content);
            if (login.UserName == "raticercvadze@gmail.com" && login.Password == "123")
            {
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, login.UserName),
                    new Claim(ClaimTypes.Email, login.Password),
                };
                ClaimsIdentity claimsIdentity = new(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new()
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

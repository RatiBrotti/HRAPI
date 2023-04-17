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
        public IActionResult Registration(string message)
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            if (claimuser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message=message;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Administrator register)
        {           
            if (register.Password != register.ConfirmPassword)
            {
                ViewBag.Message = "პაროლი არ ემთხვევა ერთმანეთს";
                return RedirectToAction("Registration", "Access", new { message = ViewBag.Message });
            }
            else if (!PasswordTools.IsValid(register.Password))
            {
                ViewBag.Message = "პაროლი არის ზედმეტად მარტივი";
                return RedirectToAction("Registration", "Access", new { message = ViewBag.Message });
            }

            register.Password = PasswordTools.MD5Hash(register.Password + "secret key");

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
                ViewBag.Message = "რეგისტრაცია წარმატებით დასრულდა, გაიარეთ ავტორიზაცია";
                return RedirectToAction("Login", "Access", new { message = ViewBag.Message });
            }
            else
            {
                ViewBag.Message = await response.Content.ReadAsStringAsync();

                return RedirectToAction("Login", "Access", new { message = ViewBag.Message });

            }
        }

        [HttpGet]
        public IActionResult Login(string message)
        {
            
            ClaimsPrincipal claimuser = HttpContext.User;
            if (claimuser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel login)
        {

            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler)
            {
                // Set the base URL of the API endpoint
                BaseAddress = new Uri("https://localhost:7071")
            };

            // Make a GET request to the API endpoint
            var response = await client.GetAsync("/api/Administrator/GetAdministratorByUserName/"+login.UserName);

            // Read the response content as a string
            var content = await response.Content.ReadAsStringAsync();
            //deserialize
            Administrator administrator = JsonConvert.DeserializeObject<Administrator>(content);

            if (login.UserName == administrator.IdNumber || login.UserName.ToLower() == administrator.Email && PasswordTools.MD5Hash(login.Password+"secret key") == administrator.Password)
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

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                ViewBag.Message = "გამარჯობა "+administrator.Name;

                return RedirectToAction("Index", "Home", new {message = ViewBag.Message});
            }
            else
            {
                ViewBag.Message = "მომხმარებლის სახელი ან პაროლი არასწორია ";
            }
            return RedirectToAction("Login","Access", new { message = ViewBag.Message });
        }
    }
}

using HRMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HRMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:11176");

        private readonly HttpClient _client;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            // Create an instance of HttpClientHandler with SSL certificate validation disabled
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            // Set the base URL of the API endpoint
            client.BaseAddress = new Uri("https://localhost:7071");

            // Make a GET request to the API endpoint
            var response = await client.GetAsync("/api/Emploee/Get");

            // Read the response content as a string
            var content = await response.Content.ReadAsStringAsync();
            //Employee employee = JsonConvert.DeserializeObject<Employee>(content);

            // Return the response content as a view or JSON data
            return Content(content); // or return Json(content);
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EmployeeDetails(string idNumber)
        {
            Employee employee = new()
            {
                IdNumber = idNumber
            };
            return View(employee);
        }
        [HttpGet]
        public IActionResult EditEmployee(string idNumber)
        {
            var employee = new Employee
            {
                IdNumber = idNumber,
                Gender = true
            };
            return View(employee);
        }
        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {
           
            return RedirectToAction("index", "Home");
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
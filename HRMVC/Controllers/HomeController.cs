using HRMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;

namespace HRMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //http request, disabling ssl
        public HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {

            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            // Set the base URL of the API endpoint
            client.BaseAddress = new Uri("https://localhost:7071");

            // Make a GET request to the API endpoint
            var response = await client.GetAsync("/api/Emploee/Get");

            // Read the response content as a string
            var content = await response.Content.ReadAsStringAsync();
            List<Employee> employee = JsonConvert.DeserializeObject<List<Employee>>(content);

            return View(employee);
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee(Employee employee)
        {
            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            //create json
            string json = JsonConvert.SerializeObject(employee);

            //create string with json
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //post request
            var response = await client.PostAsync("https://localhost:7071/api/Emploee/CreateEmployee", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return Content("Error creating employee: " + await response.Content.ReadAsStringAsync());

            }
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
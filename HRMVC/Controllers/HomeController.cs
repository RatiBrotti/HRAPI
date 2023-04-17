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
        public async Task<ActionResult> Index(string message)
        {
            ViewBag.Message = message;
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
        public async Task<ActionResult> Search(string searchWord)
        {
            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            // Set the base URL of the API endpoint
            client.BaseAddress = new Uri("https://localhost:7071");

            // Make a GET request to the API endpoint
            var response = await client.GetAsync("/api/Emploee/Find/"+searchWord);

            // Read the response content as a string
            var content = await response.Content.ReadAsStringAsync();
            List<Employee> employee = JsonConvert.DeserializeObject<List<Employee>>(content);
            if (response.IsSuccessStatusCode)
            {
                return View(employee);
            }
            else if (employee == null)
            {
                ViewBag.Message = "ჩანაწერი ვერ მოიძებნა";
                return RedirectToAction("Index", "Home", new {message=ViewBag.Message});  
            }
            else
            {
                ViewBag.Message = await response.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "Home",new {message=ViewBag.Message});
            }

            
        }

        [HttpGet]
        public IActionResult CreateEmployee(string message)
        {
            ViewBag.Message = message;
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
                ViewBag.Message = "თანამშრომელი წარმატებით დაემატა";
                return RedirectToAction("Index", "Home", new { message = ViewBag.Message });
            }
            else
            {
                ViewBag.Message = await response.Content.ReadAsStringAsync();
                return RedirectToAction("CreateEmployee", "Home", new { message = ViewBag.Message });

            }
        }

        [HttpGet]
        public async Task<ActionResult> EmployeeDetails(string idNumber, string? message)
        {
            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            // Set the base URL of the API endpoint
            client.BaseAddress = new Uri("https://localhost:7071");

            // Make a GET request to the API endpoint
            var response = await client.GetAsync("/api/Emploee/GetEmployee/" + idNumber);

            // Read the response content as a string
            var content = await response.Content.ReadAsStringAsync();
            Employee employee = JsonConvert.DeserializeObject<Employee>(content);

            return View(employee);
        }
        [HttpGet]
        public async Task<ActionResult> EditEmployee(string idNumber, string? message)
        {
            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            // Set the base URL of the API endpoint
            client.BaseAddress = new Uri("https://localhost:7071");

            // Make a GET request to the API endpoint
            var response = await client.GetAsync("/api/Emploee/GetEmployee/" + idNumber);

            // Read the response content as a string
            var content = await response.Content.ReadAsStringAsync();
            Employee employee = new();
            employee = JsonConvert.DeserializeObject<Employee>(content);
            if (message != null)
            {
                ViewBag.Message= message;
                return View(employee);
            }

            return View(employee);
        }
        [HttpPost]
        public async Task<ActionResult> EditEmployee(Employee employee)
        {
            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            //create json
            string json = JsonConvert.SerializeObject(employee);

            //create string with json
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //post request
            var response = await client.PutAsync("https://localhost:7071/api/Emploee/UpdateEmployee", content);


            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "თანამშრომელი წარმატებით განახლდა";
                return RedirectToAction("Index", "Home", new { message = ViewBag.Message });
            }
            else
            {
                ViewBag.Message = await response.Content.ReadAsStringAsync();
                return RedirectToAction("EditEmployee", "Home", new { message = ViewBag.Message, idNUmber=employee.IdNumber });

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public async Task<ActionResult> DeleteEmployee(string idNumber)
        {
            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            // Set the base URL of the API endpoint
            client.BaseAddress = new Uri("https://localhost:7071");

            // Make a GET request to the API endpoint
            var response = await client.GetAsync("/api/Emploee/GetEmployee/" + idNumber);

            // Read the response content as a string
            var content = await response.Content.ReadAsStringAsync();
            Employee employee = new();
            employee = JsonConvert.DeserializeObject<Employee>(content);
            return View(employee);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteEmployee(Employee employee)
        {
            // Create an instance of HttpClient using the above handler
            var client = new HttpClient(handler);

            // Set the base URL of the API endpoint
            client.BaseAddress = new Uri("https://localhost:7071");

            // Make a GET request to the API endpoint
            var response = await client.DeleteAsync("/api/Emploee/DeleteEmployee/" + employee.IdNumber);

            // Read the response content as a string
            var content = await response.Content.ReadAsStringAsync();
 
            var deletedEmployee = JsonConvert.DeserializeObject<Employee>(content);
            if(response.IsSuccessStatusCode)
            {
                ViewBag.Message = "ჩანაწერი წარმატებით წაიშალა";
                return RedirectToAction("Index", "Home", new {message=ViewBag.Message});
            }
            else
            {
                ViewBag.Message = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index", "Home", new { message = ViewBag.Message });
            }
            
        }
    }
}
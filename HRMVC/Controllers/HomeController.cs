using HRMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace HRMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44342/api");

        private readonly HttpClient _client;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<Employee> employeeList = new List<Employee>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Employee/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employeeList = JsonConvert.DeserializeObject<List<Employee>>(data);
            }
            return View(employeeList);
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
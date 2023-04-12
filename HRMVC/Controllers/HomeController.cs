using HRAPI.Models;
using HRMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace HRMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        HttpClientHandler _clientHandler = new HttpClientHandler();

        Administrator _administrator = new Administrator();

        List<Administrator> _administrators = new List<Administrator>();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<List<Administrator>> GetAllAdministrators()
        {
            using (var client = new HttpClient(_clientHandler))
            {
                using(var response = client.GetAsync("https://localhost:7071/api/Administrator").Result)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _administrator = JsonConvert.DeserializeObject<List<Administrator>>(apiResponse);
                }

            }

            return _administrators;

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
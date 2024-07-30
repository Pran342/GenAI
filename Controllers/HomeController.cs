using GenAITech.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GenAITech.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GenAI_Tools()
        {
            return View();
        }

        public IActionResult GenAI_Organisations()
        {
            return View();
        }
        public IActionResult GenAI_Applications()
        {
            return View();
        }

        public IActionResult AI_Jobs() 
        {
            return View();
        }

        public IActionResult Contact_Us()
        {
            return View();
        }
        public IActionResult site()
        {
            return View();
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using train1.Models;
namespace train1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger,
            ApplicationContext context )
        {
            _logger = logger;
        }
        public IActionResult HomePage()
        {
            // shopping page
           HttpContext.Session.SetString("session","Mohamed khaled hantash");
            return View();
        }

        public IActionResult Index()
        {
            // home page in admin dashbord
            return View();
        }
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
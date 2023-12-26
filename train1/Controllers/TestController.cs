using Microsoft.AspNetCore.Mvc;

namespace train1.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Teacher()
        {
            return View();
        }
        public IActionResult Student()
        {
            return View();
        }
    }
}

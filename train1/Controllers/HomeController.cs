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
        private readonly ApplicationContext _context;
        public HomeController(ILogger<HomeController> logger,
            ApplicationContext context )
        {
            _logger = logger;
            this._context = context;
        }
        //[Authorize]
        public IActionResult Index()
        {
            List<ApplicationUser> user = _context.Users.ToList();
            return View(user);
        }
        // Edit
        public IActionResult Edit(string id )
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(ApplicationUser oldUser)
        {
            if (ModelState.IsValid)
            {
                //_context.Users.Update(oldUser);
                //_context.SaveChanges();
                var newUser = _context.Users.FirstOrDefault(u => u.Id == oldUser.Id);
                newUser.FirstName= oldUser.FirstName;
                newUser.LastName= oldUser.LastName;
                newUser.Email= oldUser.Email;
                newUser.Age= oldUser.Age;
                newUser.UserName= oldUser.UserName;
                _context.Users.Update(newUser);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oldUser);
        }
        public IActionResult Delete (string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
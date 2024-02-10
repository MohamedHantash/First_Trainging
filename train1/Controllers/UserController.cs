using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using train1.Models;
using train1.Repository;

namespace train1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationContext _context;
        public UserController(IUserRepository userRepository,ApplicationContext context)
        {
            this._userRepository = userRepository;
            this._context = context;
        }
        public IActionResult GetAll()
        {
            List<ApplicationUser> users = _userRepository.GetAll();
            return View(users); 
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Add(user);
                _userRepository.Save();
                return RedirectToAction("GetAll");
            }
            return View(user);
        }

        public IActionResult Edit(string id)
        {
            ApplicationUser user =_userRepository.GetById(id);
            return View(user); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUser user)
        {
            if(ModelState.IsValid)
            {
                _userRepository.Update(user);
                _userRepository.Save();
                return RedirectToAction("GetAll");
            }
            return View(user);
        }
        public IActionResult Delete(string id)
        {
            ApplicationUser user=_userRepository.GetById(id);
            if (user!=null)
            {
                _userRepository.Delete(id);
                _userRepository.Save();
            }
            return RedirectToAction("GetAll");
        }
    }
}

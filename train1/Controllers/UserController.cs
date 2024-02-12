using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using train1.Models;
using train1.Repository;
using train1.ViewModel;

namespace train1.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IUserRepository userRepository,
               UserManager<ApplicationUser> userManager,
               SignInManager<ApplicationUser> signInManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public IActionResult GetAll()
        {
            List<ApplicationUser> users = _userRepository.GetAll();
            return View(users); 
        }
      
        public IActionResult Registe()
        {
            var list = new SelectList(_userRepository.GetAllRole(), "Id", "Name");
            ViewData["roleList"] = list;
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registe(RegisterViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser()
                {
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    Age = userVM.Age,
                    Email = userVM.Email,
                    UserName = userVM.UserName,
                    PasswordHash = userVM.Password,
                    Address = userVM.Address,
                };
                IdentityResult result = await _userManager.CreateAsync(userModel, userVM.Password);
                if (result.Succeeded)
                {
                    if (userVM.RoleName == "admin")
                    {
                        await _userManager.AddToRoleAsync(userModel, "admin");
                    }
                    else if (userVM.RoleName == "employee")
                    {
                        await _userManager.AddToRoleAsync(userModel, "employee");
                    }
                    else if (userVM.RoleName == "user")
                    {
                        await _userManager.AddToRoleAsync(userModel, "user");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(userModel, "user");
                    }
                    await _signInManager.SignInAsync(userModel, isPersistent: false);
                    return RedirectToAction("GetAll");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            var list = new SelectList(_userRepository.GetAllRole(), "Id", "Name");
            ViewData["roleList"] = list;
                    
            return View(userVM);
        }
        public IActionResult Edit(string id)
        {
            ApplicationUser user =_userRepository.GetById(id);
            return View(user); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser userModel= await _userManager.FindByNameAsync(user.UserName);
                if(userModel != null)
                {
                    await _userManager.UpdateAsync(userModel);
                    _userRepository.Save();
                    return RedirectToAction("GetAll");
                }
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

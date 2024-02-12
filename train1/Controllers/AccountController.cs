using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using train1.Models;
using train1.ViewModel;
using train1.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace train1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public AccountController( UserManager<ApplicationUser> userManager
                                , SignInManager<ApplicationUser> signInManager )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = 
                     await _userManager.FindByNameAsync(userVM.UserName);
                if (userModel!=null) 
                {
                    bool found=await  _userManager.CheckPasswordAsync(userModel,userVM.Password);
                    if (found)
                    {
                       await _signInManager.SignInAsync(userModel, userVM.RememberMe);
                        return RedirectToAction("GetAll","User");
                    }
                    else
                        ModelState.AddModelError("", "UserName or Password is incorrect");
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password is incorrect");
                }

            }
            return View(userVM);
        }
        public async Task<IActionResult> SignOut()
        {
             await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
    }
}

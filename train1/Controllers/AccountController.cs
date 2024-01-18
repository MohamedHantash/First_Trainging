using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using train1.Models;
using train1.ViewModel;

namespace train1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Registe()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Registe(RegisterViewModel userVM)
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
                IdentityResult result= await  _userManager.CreateAsync(userModel, userVM.Password);
                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(userModel, "user");
                    await _signInManager.SignInAsync(userModel,isPersistent:false);
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                    //ModelState.AddModelError("", result.Errors.FirstOrDefault().Description);
                }
                
            }
            return View(userVM);
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
                        //if (User.IsInRole("admin"))
                        //else if(User.IsInRole("teacher"))
                        //else if (User.IsInRole("student"))

                        return RedirectToAction("Index", "Home");

                    }
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

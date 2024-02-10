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
        private readonly IAccountRepository _accountRepository;
        public AccountController( UserManager<ApplicationUser> userManager
                                , SignInManager<ApplicationUser> signInManager
                                , IAccountRepository accountRepository)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._accountRepository = accountRepository;
        }

        public IActionResult Registe()
        {
            // ViewData["roleList"]=new SelectList( _accountRepository.GetRoles(),nameof(IdentityRole.Id), nameof(IdentityRole.Name)); 
            ViewData["roleList"] = new SelectList(_accountRepository.GetRoles());
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
                    if (userVM.RoleName == "admin")
                    {
                        await _userManager.AddToRoleAsync(userModel, "admin");
                    }
                    else if (userVM.RoleName =="employee")
                    {
                        await _userManager.AddToRoleAsync(userModel, "employee");
                    }
                    else if(userVM.RoleName == "user")
                    {
                       await _userManager.AddToRoleAsync(userModel, "user");
                    }
                    else
                    {
                       await _userManager.AddToRoleAsync(userModel, "user");
                    }
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
            ViewData["roleList"] = _accountRepository.GetRoles();
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

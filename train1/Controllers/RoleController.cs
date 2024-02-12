using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using train1.Models;
using train1.ViewModel;

namespace train1.Controllers
{

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager) 
        { 
            this._roleManager = roleManager;
        }
        
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel newRole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleModel = new IdentityRole();
                roleModel.Name = newRole.RoleName;
                IdentityResult result= await _roleManager.CreateAsync(roleModel);
                if (result.Succeeded)
                {
                    return View();
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
    }
}

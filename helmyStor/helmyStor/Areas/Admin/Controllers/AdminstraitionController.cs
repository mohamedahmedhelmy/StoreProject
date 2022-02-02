using Domains;
using helmyStor.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace helmyStor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminstraitionController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminstraitionController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> user)
        {
            this.roleManager = roleManager;
            this.userManager = user;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = model.RoleName
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Adminstraition");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.Error = $"Role with Id {model.Id} Not Found ";
                return View("Not Found");
            }
            role.Name = model.RoleName;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles", "Adminstraition");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role= await roleManager.FindByIdAsync(Id);
            if (role==null)
            {
                return NotFound();
            }
            var result=await roleManager.DeleteAsync(role);
            return RedirectToAction("ListRoles", "Adminstraition");
        }
        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string Id)
        {
            var role=await roleManager.FindByIdAsync(Id);
            ViewBag.Id = Id;
            if (role == null)
            {
                return NotFound();
            }
            var Userlist=new List<UserInRoleViewModel>();
            foreach (var user in userManager.Users.ToList())
            {
                var userInRole = new UserInRoleViewModel()
                {
                    UserId=user.Id,
                    UserName=user.UserName
                };
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected=true;
                }
                else
                {
                    userInRole.IsSelected=false;
                }
                Userlist.Add(userInRole);

            }
           return View(Userlist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserInRole(List<UserInRoleViewModel> userList ,string Id)
        {
            var role= await roleManager.FindByIdAsync(Id);
            foreach (var userRole in userList)
            {
                var userRoleVM = userRole;
                var user = await userManager.FindByIdAsync(userRoleVM.UserId);
                if (userRoleVM.IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    await userManager.AddToRoleAsync(user,role.Name);
                }
                else if ( !(userRoleVM.IsSelected) && await userManager.IsInRoleAsync(user, role.Name))
                {
                    await userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
            return RedirectToAction("EditRole", new { Id = Id });
        }
    }
}   

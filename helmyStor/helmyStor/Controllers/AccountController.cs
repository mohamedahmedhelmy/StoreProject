using Microsoft.AspNetCore.Mvc;
using helmyStor.ViewModel;
using Microsoft.AspNetCore.Identity;
using Domains;
using Microsoft.AspNetCore.Authorization;

namespace helmyStor.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    { 
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> user, SignInManager<ApplicationUser> signIn)
        {
            userManager = user;
            signInManager = signIn;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(User, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string ReturnUrl)
        {
            if (ModelState.IsValid) {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return LocalRedirect(ReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                        ModelState.AddModelError(string.Empty,"Invalid Email Or Password");
            }

         return View(model);
        }
        [AcceptVerbs("Get","Post")]
        public async Task<IActionResult> IsEmailExist(string email)
        {
            var User =await userManager.FindByNameAsync(email);
            if (User == null)
                return Json(true);
            else
                return Json($"The Email {email} Already Use");

        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}

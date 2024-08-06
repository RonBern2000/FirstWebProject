using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebProject.Filters;
using WebProject.Models;

namespace WebProject.Controllers
{
    [ServiceFilter(typeof(ActionsFilter))]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult LoginForm()
        {
             return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var res = await _signInManager.PasswordSignInAsync(user.Username!, user.Password!,false,false);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LoginForm", "Account");
        }
    }
}

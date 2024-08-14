using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebProject.Filters;
using ZooLib.Models;

namespace WebProject.Controllers
{
    [ServiceFilter(typeof(ActionsFilter))]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
             return View("LoginForm");
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var res = await _signInManager.PasswordSignInAsync(user.Username!, user.Password!,false,false);
                if (res.Succeeded)
                {
                    var loggedUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
                    string jwt = GenerateToken(loggedUser!);
                    CreateCookie(jwt);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        
        public IActionResult AccessDenied()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login");
        }

        private void CreateCookie(string jwt)
        {
            Response.Cookies.Append("JWT", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            });
        }
        private string GenerateToken(IdentityUser dbUser)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, dbUser.UserName!)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken
                (
                issuer: _configuration["jwt:Issuer"],
                audience: _configuration["jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

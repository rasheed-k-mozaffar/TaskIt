using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskIt.Web.Controllers
{
    public class UserController : Controller
    {
        //Using dependency injection to inject the I user interface and use its implementaion class to register , sign in and log users out
        private SignInManager<IdentityUser> SignInManager;
        private UserManager<IdentityUser> UserManager;

        public UserController(SignInManager<IdentityUser> _signInManager , UserManager<IdentityUser> _userManager)
        {
            SignInManager = _signInManager;
            UserManager = _userManager;
        }


        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    
                    return RedirectToAction("SignIn");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult SignIn() => View();

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginUserModel model)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Todos");
                }

                ModelState.AddModelError("", "UserName or Password is Incorrect");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Logout() => View();

        [HttpPost]
        public async Task<IActionResult> LogoutPOST()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DontLogout()
        {
            return RedirectToAction("Index", "Todos");
        }
        
    }
}


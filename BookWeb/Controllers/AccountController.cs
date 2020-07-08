using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookWeb.Models;
using BookWeb.Interface;
using BookWeb.Entities;
using BookWeb.Dto;
using Microsoft.AspNetCore.Identity;
using BookWeb.Enums;

namespace BookWeb.Controllers
{
    public class AccountController : BaseController
    {

        private readonly IAccount _account;

        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<ApplicationRole> _roleManager;
        //string Message = "";

        //public AccountController(SignInManager<ApplicationUser> signInManager,
        //    RoleManager<ApplicationRole> roleManager,
        //    UserManager<ApplicationUser> userManager)
        //{
        //    _signInManager = signInManager;
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //}


        public AccountController(IAccount account, SignInManager<ApplicationUser> signInManager)
        {
            _account = account;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "UserName/Password is incorrect");
                return View();
            }

            var signin = await _account.LoginIn(login);

            if (signin)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();



        }
        public IActionResult Signup()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Signup( SigninViewModel signupmodel)
        {
            ApplicationUser user = new ApplicationUser();

            user.UserName = signupmodel.UserName;
            user.Email = signupmodel.Email;

            var sign = await _account.CreateUser(user, signupmodel.Password);
            if (sign)
            {
                //Alert("Account Created successfully", NotificationType.success);
                return RedirectToAction("Index", "Home");

            }
            Alert("Account not created!", NotificationType.error);
            return View();
            //ApplicationUser user = new ApplicationUser();


            //user.UserName = signup.Username;
            //user.Email = signup.Email;


            //var newUser = await _account.CreateUser(user, signup.Password);
            //if (newUser)
            //{
            //    Alert("Welcome "+ signup.Username + "!", NotificationType.success);
            //    return RedirectToAction("Index", "Home");
            //}

            //Alert("Author not created!", NotificationType.error);
            //return View();
        }




        //[HttpPost]
        //public async Task<IActionResult> Signup([FromBody] UserDto registerUser)
        //{
        //    ApplicationUser user = new ApplicationUser();


        //    user.UserName = registerUser.Username;
        //    user.Email = registerUser.Email;


        //    var newUser = await _account.CreateUser(user, registerUser.Password);
        //    if (newUser)
        //        return RedirectToAction("Index", "Home");


        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");


        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

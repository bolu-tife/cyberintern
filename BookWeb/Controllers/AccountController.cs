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

namespace BookWeb.Controllers
{
    public class AccountController : Controller
    {
        private IAccount _account;
        public AccountController(IAccount account)
        {
            _account = account;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _account.GetAll();

            if (model != null)
                return View(model);
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> Create(UserDto registerUser)
        //{

        //    //var createAuthor = await _account.AddAsync(registerUser);

        //    //if (createAuthor)
        //    //{
        //    //    return RedirectToAction("Index");
        //    //}
        //    return View();
        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

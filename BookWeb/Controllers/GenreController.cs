using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookWeb.Models;
using BookWeb.Interface;
using BookWeb.Entities;
namespace BookWeb.Controllers
{
    public class GenreController : Controller
    {
        private IGenre _genre;
        public GenreController(IGenre genre)
        {
            _genre = genre;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _genre.GetAll();

            if (model != null)
                return View(model);
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {

            var createCategory = await _genre.AddAsync(genre);

            if (createCategory)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

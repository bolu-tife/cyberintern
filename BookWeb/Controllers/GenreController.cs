using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookWeb.Models;
using BookWeb.Interface;
using BookWeb.Entities;
using Microsoft.AspNetCore.Identity;
using BookWeb.Enums;

namespace BookWeb.Controllers
{
    public class GenreController : BaseController
    {
        private IGenre _genre;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public GenreController(IGenre genre, UserManager<ApplicationUser> userManager)
        {
            _genre = genre;
            _userManager = userManager;
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
            genre.CreatedBy = _userManager.GetUserName(User);

            var createGenre = await _genre.AddAsync(genre);

            if (createGenre)
            {
                Alert("Genre Created successfully!", NotificationType.success);
                return RedirectToAction("Index");
            }
            Alert("Genre not created successfully!", NotificationType.error);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editGenre = await _genre.GetById(id);

            if (editGenre == null)
            {
                return RedirectToAction("Index");
            }

            return View(editGenre);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Genre genre)
        {
            //var editAuthor = await _genre.GetById(id);
            var editGenre = await _genre.Update(genre);

            if (editGenre && ModelState.IsValid)
            {
                //    editAuthor.Name = author.Name;
                //    context.SaveChanges();
                Alert("Genre edited successfully!", NotificationType.success);
                return RedirectToAction("Index");
                //return RedirectToAction("Details", new { id = editAuthor.Id });
            }
            Alert("Genre not edited successfully!", NotificationType.error);
            return View();
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var deleteGenre = await _genre.Delete(id);

            if (deleteGenre)
            {
                Alert("Genre deleted successfully!", NotificationType.success);
                return RedirectToAction("Index");
            }
            Alert("Genre not deleted successfully!", NotificationType.error);
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

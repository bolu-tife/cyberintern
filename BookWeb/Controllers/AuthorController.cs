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
    public class AuthorController : Controller
    {
        private IAuthor _author;
        public AuthorController(IAuthor author)
        {
            _author = author;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _author.GetAll();

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
        public async Task<IActionResult> Create(Author author)
        {

            var createAuthor = await _author.AddAsync(author);

            if (createAuthor)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editAuthor = await _author.GetById(id);

            if (editAuthor == null)
            {
                return RedirectToAction("Index");
            }
            return View(editAuthor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            //var editAuthor = await _author.GetById(id);
            var editAuthor = await _author.Update(author);

            if (editAuthor && ModelState.IsValid)
            {
                //    editAuthor.Name = author.Name;
                //    context.SaveChanges();
                return RedirectToAction("Index");
                //return RedirectToAction("Details", new { id = editAuthor.Id });
            }
            return View();
        }

        //[HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var deleteAuthor = await _author.Delete(id);

            if (deleteAuthor)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

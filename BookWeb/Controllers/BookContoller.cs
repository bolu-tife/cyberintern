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
    public class BookController : Controller
    {
        private IBook _book;
        public BookController(IBook book)
        {
            _book = book;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _book.GetAll();

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
        public async Task<IActionResult> Create(Book book)
        {

            var createBook = await _book.AddAsync(book);

            if (createBook)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editBook = await _book.GetById(id);

            if (editBook == null)
            {
                return RedirectToAction("Index");
            }
            return View(editBook);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            //var editAuthor = await _author.GetById(id);
            var editBook = await _book.Update(book);

            if (editBook && ModelState.IsValid)
            {
                //    editAuthor.Name = author.Name;
                //    context.SaveChanges();
                return RedirectToAction("Index");
                //return RedirectToAction("Details", new { id = editAuthor.Id });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteBook = await _book.Delete(id);

            if (deleteBook)
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

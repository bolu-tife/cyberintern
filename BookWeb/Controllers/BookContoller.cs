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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookWeb.Controllers
{
    public class BookController : BaseController
    {
        private IBook _book;
        private IAuthor _author;
        private IGenre _genre;
        private IPublisher _publisher;
        private readonly UserManager<ApplicationUser> _userManager;
        public BookController(IBook book, IAuthor author, IGenre genre, IPublisher publisher, UserManager<ApplicationUser> userManager)
        {
            _book = book;
            _author = author;
            _genre = genre;
            _publisher = publisher;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _book.GetAll();

            if (model != null)
                return View(model);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var author = await _author.GetAll();
            var genre = await _genre.GetAll();
            var publisher = await _publisher.GetAll();
            var authorList = author.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Title + " " + a.Name
            });

            var genreList = genre.Select(g => new SelectListItem()
            {
                Value = g.Id.ToString(),
                Text = g.Name
            });

            var publisherList = publisher.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.PublisherName
            });

            ViewBag.author = authorList;
            ViewBag.genre = genreList;
            ViewBag.publisher = publisherList;
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            book.CreatedBy = _userManager.GetUserName(User);

            var createBook = await _book.AddAsync(book);
           
            if (createBook)
            {
                Alert("Book created successfully.", NotificationType.success);
                return RedirectToAction("Index");
            }
            Alert("Book not created!", NotificationType.error);
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
            var author = await _author.GetAll();
            var authorList = author.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Title + " " + a.Name
            });
            var genre = await _genre.GetAll();
            var genreList = genre.Select(g => new SelectListItem()
            {
                Value = g.Id.ToString(),
                Text = g.Name
            });
            var publisher = await _publisher.GetAll();
            var publisherList = publisher.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.PublisherName
            });

            ViewBag.author = authorList;
            ViewBag.genre = genreList;
            ViewBag.publisher = publisherList;
            
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
                Alert("Book edited successfully.", NotificationType.success);
                return RedirectToAction("Index");
                //return RedirectToAction("Details", new { id = editAuthor.Id });
            }
            Alert("Book not edited!", NotificationType.error);
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteBook = await _book.Delete(id);

            if (deleteBook)
            {
                Alert("Book deleted successfully.", NotificationType.success);
                return RedirectToAction("Index");
            }
            Alert("Book not deleted!", NotificationType.error);
            return View();
        }
        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

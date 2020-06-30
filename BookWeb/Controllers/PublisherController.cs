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
    public class PublisherController : Controller
    {
        private IPublisher _publisher;
        public PublisherController(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _publisher.GetAll();

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
        public async Task<IActionResult> Create(Publisher publisher)
        {

            var createPublisher = await _publisher.AddAsync(publisher);

            if (createPublisher)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editPublisher = await _publisher.GetById(id);

            if (editPublisher == null)
            {
                return RedirectToAction("Index");
            }
            return View(editPublisher);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Publisher publisher)
        {
            //var editAuthor = await _author.GetById(id);
            var editPublisher = await _publisher.Update(publisher);

            if (editPublisher && ModelState.IsValid)
            {
                //    editAuthor.Name = author.Name;
                //    context.SaveChanges();
                return RedirectToAction("Index");
                //return RedirectToAction("Details", new { id = editAuthor.Id });
            }
            return View();
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var deletePublisher = await _publisher.Delete(id);

            if (deletePublisher)
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

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
    public class PublisherController : BaseController
    {
        private IPublisher _publisher;
        private readonly UserManager<ApplicationUser> _userManager;

        
        public PublisherController(IPublisher publisher, UserManager<ApplicationUser> userManager)
        {
            _publisher = publisher;
            _userManager = userManager;
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
            publisher.CreatedBy = _userManager.GetUserName(User);
            var createPublisher = await _publisher.AddAsync(publisher);

            if (createPublisher)
            {
                Alert("Publisher created successfully!", NotificationType.success);
                return RedirectToAction("Index");
            }
            Alert("Publisher not deleted successfully!", NotificationType.error);
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
                Alert("Publisher edited successfully!", NotificationType.success);
                return RedirectToAction("Index");
                //return RedirectToAction("Details", new { id = editAuthor.Id });
            }
            Alert("Publisher not edited successfully!", NotificationType.error);
            return View();
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var deletePublisher = await _publisher.Delete(id);

            if (deletePublisher)
            {
                Alert("Publisher deleted successfully!", NotificationType.success);
                return RedirectToAction("Index");
            }
            Alert("Publisher not deleted!", NotificationType.success);
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

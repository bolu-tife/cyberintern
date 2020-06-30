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
    public class CategoryController : Controller
    {
        private ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _category.GetAll();

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
        public async Task<IActionResult> Create(Category category)
        {

            var createCategory = await _category.AddAsync(category);

            if (createCategory)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editCategory = await _category.GetById(id);

            if (editCategory == null)
            {
                return RedirectToAction("Index");
            }
            return View(editCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            //var editAuthor = await _author.GetById(id);
            var editCategory = await _category.Update(category);

            if (editCategory && ModelState.IsValid)
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
            var deleteCategory = await _category.Delete(id);

            if (deleteCategory)
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

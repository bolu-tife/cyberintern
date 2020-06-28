using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWeb.Data;
using BookWeb.Entities;
using BookWeb.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Services
{
    public class CategoryService : ICategory
    {
        private BookWebDataContext _context;
        
        public CategoryService(BookWebDataContext context)
        {
            _context = context;
        }
        public void Add(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }
        public async Task<bool> AddAsync(Category category)
        {
            try
            {
                await _context.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Delete(int Id)
        {
           
            var category = await _context.Categories.FindAsync(Id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Category>> GetAll() //GetAll
        {

            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int Id) //GetById
        {
            var category = await _context.Categories.FindAsync(Id);

            return category;
        }

        public async Task<bool> Update(Category category) //Update
        {
            var cat = await _context.Categories.FindAsync(category.Id);
            if (cat != null)
            {
                cat.CategoryName = category.CategoryName;
                cat.Location = category.Location;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
       
    }
}

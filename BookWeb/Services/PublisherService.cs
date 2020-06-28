using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWeb.Data;
using BookWeb.Entities;
using BookWeb.Interface;
using Microsoft.EntityFrameworkCore;
namespace BookWeb.Services
{
    public class PublisherService : IPublisher
    {
        private BookWebDataContext _context;

        public PublisherService(BookWebDataContext context)
        {
            _context = context;
        }

        public void Add(Publisher publisher)
        {
            _context.Add(publisher);
            _context.SaveChanges();
        }

        public async Task<bool> AddAsync(Publisher publisher)
        {
            try
            {
                await _context.AddAsync(publisher);
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
            
            var publisher = await _context.Publishers.FindAsync(Id);

            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Publisher>> GetAll() //GetAll
        {

            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher> GetById(int Id) //GetById
        {
            var publisher = await _context.Publishers.FindAsync(Id);

            return publisher;
        }

        public async Task<bool> Update(Publisher publisher) //Update
        {
            var pub = await _context.Publishers.FindAsync(publisher.Id);
            if (pub != null)
            {
                pub.PublisherName = publisher.PublisherName;
                pub.Country = publisher.Country;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
        
    }
}

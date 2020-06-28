using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWeb.Entities;
namespace BookWeb.Interface
{
    public interface IPublisher
    {
        void Add(Publisher publisher);
        Task<bool> AddAsync(Publisher publisher);
        Task<bool> Update(Publisher publisher);
        Task<IEnumerable<Publisher>> GetAll();
        Task<Publisher> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}

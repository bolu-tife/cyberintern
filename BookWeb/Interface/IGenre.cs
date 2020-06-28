using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWeb.Entities;

namespace BookWeb.Interface
{
    public interface IGenre
    {
        
            void Add(Genre genre);
            Task<bool> AddAsync(Genre genre);
            Task<bool> Update(Genre genre);
            Task<IEnumerable<Genre>> GetAll();
            Task<Genre> GetById(int Id);
            Task<bool> Delete(int Id);
        
    }
}

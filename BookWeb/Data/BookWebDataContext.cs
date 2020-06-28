using System;
using BookWeb.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Data
{
    public class BookWebDataContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public BookWebDataContext(DbContextOptions<BookWebDataContext> options) : base(options)
        {
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<User> BookUsers { get; set; }
    }
}

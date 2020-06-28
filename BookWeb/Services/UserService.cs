using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWeb.Data;
using BookWeb.Entities;
using BookWeb.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Services
{
    public class UserService : IUser
    {
        private BookWebDataContext _context;
        public UserService(BookWebDataContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.BookUsers.SingleOrDefault(x => x.Username == username || x.Email == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

       

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_context.BookUsers.Any(x => x.Username == user.Username || x.Email == user.Email))
                throw new Exception("Username \"" + user.Username + "\" or Email \"" + user.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            //Create passwordHash and passwordSalt
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Dateregistered = DateTime.UtcNow;

            _context.BookUsers.Add(user);
            _context.SaveChanges();

            return user;
        }

       

        public async Task<bool>  Delete(int id)
        {

            var users = await _context.BookUsers.FindAsync(id);

            if (users != null)
            {
                _context.BookUsers.Remove(users);
                _context.SaveChanges();

                return true;
                
            }

            return false;
        }

        public async Task<IEnumerable<User>> GetAll() //GetAll
        {
            return  await _context.BookUsers.ToListAsync();
        }



        public async Task<User> GetById(int id)//GetById
        {
            
                var users = await _context.BookUsers.FindAsync(id);

                return users;
            
        }

        

        

        public async Task<bool> Update(User user, string password = null)
        {

            var users =  await _context.BookUsers.FindAsync(user.Id);
            if (users != null)
            {
                users.FirstName = user.FirstName;
                users.LastName = user.LastName;
                users.Username = user.Username;
                users.Email = user.Email;
                users.PhoneNo = user.PhoneNo;


                await _context.SaveChangesAsync();

                return true;
                    
            }
            return false;

            }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}


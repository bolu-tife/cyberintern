using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWeb.Dto;
using BookWeb.Entities;
using BookWeb.Models;

namespace BookWeb.Interface
{
    public interface IAccount
    {
        Task<bool> CreateUser(ApplicationUser user, string password);

        Task<SignInModel> SignIn(LoginDto loginDetails);


        User Authenticate(string username, string password);
        Task<IEnumerable<ApplicationUser>> GetAll();
        Task<ApplicationUser> GetByUserId(string id);
        Task<bool> UpdateUser(ApplicationUser user);

        Task<bool> Delete(string id);
        //Task LoginIn(LoginViewModel login);
        Task<bool> LoginIn(LoginViewModel loginModel);
        //Task AddAsync(UserDto registerUser);
    }

}


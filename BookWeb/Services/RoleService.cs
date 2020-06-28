using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWeb.Entities;
using BookWeb.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookWeb.Services
{
    public class RoleService : IRole
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private IConfiguration _config;

        public RoleService(SignInManager<ApplicationUser> signInManager,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<ApplicationRole> roleManager,
                         IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<bool> CreateRole(ApplicationRole role)
        {
            try
            {
                var checkrole = await _roleManager.CreateAsync(role);

                if (checkrole.Succeeded)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<IEnumerable<ApplicationRole>> GetAll() //GetAll
        {

            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<ApplicationRole> GetByRoleId(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            return role;
        }

        public async Task<bool> Delete(string id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id);
            if (roleToDelete == null)
            {
                return false;
            }
            else
            {
                await _roleManager.DeleteAsync(roleToDelete);
                return true;
            }

        }

        
        public async Task<bool> UpdateUser(ApplicationRole role)
        {
            var updaterole = await _roleManager.FindByIdAsync(role.Id);
            if (updaterole != null)
            {
                updaterole.Name = role.Name;


                await _roleManager.UpdateAsync(updaterole);
                return true;
            }

            return false;
        }

    }

}
       

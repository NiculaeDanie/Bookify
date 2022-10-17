using Application;
using Application.Abstract;
using Bookify.Domain.Model;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task CreateUser(User user,string password)
        {
            await userManager.CreateAsync(user, password);
        }
        public async Task<User> GetById(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<User> GetByName(string name)
        {
            return await userManager.FindByEmailAsync(name);
        }
        public async Task<bool> CheckPassword(User user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }
        public async Task<bool> CheckRole(string role) 
        {
            return await roleManager.RoleExistsAsync(role);
        }
        public async Task CreateRole(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
        }

        public async Task<IList<Claim>> GetRoles(User user)
        {
            return await userManager.GetClaimsAsync(user);
        }
    }
}

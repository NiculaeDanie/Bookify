using Bookify.Domain.Model;
using Domain;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Tsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IUserRepository
    {
        Task CreateUser(User user,string password);
        Task<User> GetById(string id);
        Task<User> GetByName(string name);
        Task<bool> CheckPassword(User user, string password);
        Task<bool> CheckRole(string role);
        Task CreateRole(IdentityRole role);
        Task<IList<Claim>> GetRoles(User user);
    }
}

using Bookify.Domain.Model;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<List<Genre>> GetUserPreferences(User user);
        Task AddBookToHistory();
    }
}

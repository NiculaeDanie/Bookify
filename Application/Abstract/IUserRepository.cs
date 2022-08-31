using Bookify.Domain.Model;
using Domain;
using Org.BouncyCastle.Tsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IUserRepository
    {
        Task<bool> VerifyEmail(string email);
        Task<List<Genre>> GetUserPreferences(User user);
        Task AddBookToHistory(Book book, User User);
        Task<User> VerifyUser(string email, string password);
        Task<List<Book>> GetUserHistory(int Userid);
        Task<List<Book>> GetUserFavorites(int Userid);
        Task Add(User user);
        Task<User> GetById(int id);
        Task AddBookToFavorites(Book book, User user);
        Task DeleteFromFavorites(Book book, User user);
    }
}

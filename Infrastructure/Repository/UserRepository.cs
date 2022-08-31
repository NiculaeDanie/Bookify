using Application;
using Application.Abstract;
using Bookify.Domain.Model;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task AddBookToFavorites(Book book, User user)
        {
            var b = await _context.Books.Include(a => a.UserFavorites).SingleOrDefaultAsync(a => a.Id == book.Id);
            var res = new UserFavorites()
            {
                UserId = user.Id,
                BookId = book.Id,
                User = user,
                Book = book
            };
            b.UserFavorites.Add(res);
        }

        public async Task AddBookToHistory(Book book, User user)
        {
            var b = await _context.Books.Include(a => a.UserBook).SingleOrDefaultAsync(a => a.Id == book.Id);
            if(b.UserBook.Any(b=>b.BookId == book.Id && b.UserId == user.Id))
            {
                return;
            }
            var res = new UserBook()
            {
                UserId = user.Id,
                BookId = book.Id,
                User = user,
                Book = book
            };
            b.UserBook.Add(res);
        }



        public async Task<User> GetById(int id)
        {
            return await _context.Users.SingleAsync(a => a.Id == id);
        }

        public async Task<List<Book>> GetUserHistory(int UserId)
        {
            var user = await _context.Users.Include(u => u.UserBook).ThenInclude(u=> u.Book).SingleAsync(u => u.Id == UserId);
            return user.UserBook.Select(x => x.Book).ToList();
        }
        public async Task<List<Book>> GetUserFavorites(int UserId)
        {
            var user = await _context.Users.Include(u => u.UserFavorites).ThenInclude(u => u.Book).SingleAsync(u => u.Id == UserId);
            return user.UserBook.Select(x => x.Book).ToList();
        }

        public Task<List<Genre>> GetUserPreferences(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> VerifyUser(string email, string password)
        {
            return await _context.Users.SingleAsync(a => a.Email == email && a.Password == password);
        }

        public async Task DeleteFromFavorites(Book book, User user)
        {
            var u = await _context.Users.Include(u => u.UserFavorites).SingleAsync(u => u.Id == user.Id);
            u.UserFavorites.Remove(u.UserFavorites.Where(u => u.BookId == book.Id).SingleOrDefault());
        }
        public async Task<bool> VerifyEmail(string email)
        {
            return  _context.Users.Any(u => u.Email == email);
        }
    }
}

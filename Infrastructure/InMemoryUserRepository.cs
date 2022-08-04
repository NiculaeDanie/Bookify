﻿using Application;
using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _user = new();
        public void AddBookToHistory(int id, Book book)
        {
            var user = _user.FirstOrDefault(u => u.id == id);
            user.history.Add(book);
        }

        public void CreateUser(User user)
        {
            _user.Add(user);
            user.id = _user.Count();
        }
    }
}

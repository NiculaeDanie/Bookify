﻿using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        void AddBookToHistory(int id, Book book);
        int VerifyUser(string email, string password);
        IEnumerable<Book> GetHistory(int id);
        User GetUser(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.Abstract;
using Bookify.Domain.Model;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly DataContext _context;
        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(Author author)
        {
            await _context.Authors.AddAsync(author);
        }

        public async Task AddBookToAuthor(Author author, Book book)
        {
            var res = new AuthorBook()
            {
                AuthorId = author.Id,
                BookId = book.Id,
                Author = author,
                Book = book
            };
            author.AuthorBook.Add(res);
        }

        public async Task<List<Author>> GetAll()
        {
            return await _context.Authors.Include(p=>p.AuthorBook).Take(100).ToListAsync();
        }

        public async Task<Author> GetById(int AuthorId)
        {
            var author = await _context.Authors.SingleOrDefaultAsync(a => a.Id == AuthorId);
            return author;
        }
    }
}

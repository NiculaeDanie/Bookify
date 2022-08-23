using Application;
using Application.Abstract;
using Bookify.Domain.Model;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Add(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Books.Take(100).ToListAsync();
        }

        public async Task<List<Book>> GetBookByGenre(Genre genre)
        {
            var books = await _context.Books
                .Include(b => b.BookGenre)
                .ThenInclude(BookGenre => BookGenre.Genre)
                .ToListAsync();

            return books.Where(p=> p.BookGenre.Any(b=>b.Genre==genre)).ToList();
        }

        public Task<IFormFile> GetBookContent(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> GetById(int id)
        {
            return await _context.Books.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task PublishBook(Book book)
        {
            book.PublishBook();
            _context.Update(book);
        }
    }
}

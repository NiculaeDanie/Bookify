using Application;
using Application.Abstract;
using Bookify.Domain.Model;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public async Task<Book> AddGenreToBook(Genre genre, Book book)
        {
            var b = await _context.Books.Include(a => a.BookGenre).SingleOrDefaultAsync(a => a.Id == book.Id);
            var res = new BookGenre()
            {
                GenreId = genre.Id,
                BookId = book.Id,
                Genre = genre,
                Book = book
            };
            b.BookGenre.Add(res);
            return b;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Books.OrderByDescending(x=>x.ViewCount).ToListAsync();
        }

        public async Task<List<Book>> GetBookByGenre(Genre genre)
        {
            var book = await _context.Books.Include(x => x.AuthorBook).ThenInclude(x => x.Author).Include(x => x.BookGenre).ThenInclude(x => x.Genre).Where(a => a.BookGenre.Any(x => x.GenreId == genre.Id)).OrderByDescending(x => x.ViewCount).ToListAsync();
            return book;
        }


        public async Task<List<Book>> GetBookByGenre(Genre genre,List<Book> history)
        {
            var book = await _context.Books.Include(x => x.AuthorBook).ThenInclude(x => x.Author).Include(x => x.BookGenre).ThenInclude(x => x.Genre).Where(a => a.BookGenre.Any(x=> x.GenreId == genre.Id) ).OrderByDescending(x=>x.ViewCount).Except(history).ToListAsync();
            return book;
        }
        public async Task<List<Book>> GetBookByAuthor(Author author)
        {
            var book = await _context.Books.Include(x => x.AuthorBook).ThenInclude(x => x.Author).Include(x => x.BookGenre).ThenInclude(x => x.Genre).Where(a => a.AuthorBook.Any(x => x.AuthorId == author.Id)).OrderByDescending(x => x.ViewCount).ToListAsync();
            return book;
        }

        public async Task<byte[]> GetBookContent(int book)
        {
            var b = await _context.Books.SingleOrDefaultAsync(b => b.Id == book);

            return b.Content;
        }

        public async Task<Book> GetById(int id)
        {
            return await _context.Books.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task IncrementViewCount(Book book)
        {
            book.Increment();
            _context.Update(book);
        }

        public async Task Remove(Book book)
        {
            _context.Books.Remove(book);
        }

        public async Task<List<Book>> Search(string search)
        {
            return await _context.Books.Where(b=> b.Title.Contains(search) || b.Description.Contains(search)).ToListAsync();
        }

        public async Task Update(Book book)
        {
            _context.Books.Update(book);
        }
    }
}

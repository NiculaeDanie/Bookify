using Application;
using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly List<Book> _book=new();
        public void CreateBook(Book book)
        {
            _book.Add(book);
            book.id = _book.Count;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _book;
        }

        public Book GetBook(int id)
        {
            return _book.FirstOrDefault(b => b.id == id);
        }

        public void PublishBook(int id)
        {
            var book = _book.FirstOrDefault(b => b.id==id);
            book.PublishBook();
        }
        public string GetContent(int id)
        {
            var book = _book.FirstOrDefault(b => b.id == id);
            return book.content;
        }
    }
}

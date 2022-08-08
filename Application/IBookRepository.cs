using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IBookRepository
    {
        void CreateBook(Book book);
        Book GetBook(int id);
        IEnumerable<Book> GetAllBooks();
        void PublishBook(int id);
        string GetContent(int id);
    }
}

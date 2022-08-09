using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Design_Patterns
{
    public class BookFactory : IBookFactory
    {
        public IBook CreateBook(string title, string contents) => new Book
        {
            title = title,
            content = contents
        };
    }
}

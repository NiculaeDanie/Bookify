using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IAuthorRepository
    {
        void CreateAuthor(Author author);
        void AddBookToAuthor(int authorId,Book book);
        Author GetAuthor(int authorId);
        IEnumerable<Author> GetAuthors();
    }
}

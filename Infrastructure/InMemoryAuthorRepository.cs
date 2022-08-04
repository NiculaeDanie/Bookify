using Application;
using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class InMemoryAuthorRepository : IAuthorRepository
    {
        private readonly List<Author> _authors = new();
        public void AddBookToAuthor(int authorId, Book book)
        {
            var author = _authors.FirstOrDefault(u => u.id == authorId);
            author.books.Add(book);
        }

        public void CreateAuthor(Author author)
        {
            _authors.Add(author);
            author.id = _authors.Count;
        }

        public Author GetAuthor(int authorId)
        {
            return _authors.FirstOrDefault(u => u.id == authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _authors;
        }

 
    }
}

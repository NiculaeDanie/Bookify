using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IAuthorRepository
    {
        Task<Author> GetById(int AuthorId);
        Task<List<Author>> GetAll();
        Task Add(Author author);
        Task AddBookToAuthor(Author author, Book book);
        Task<List<Book>> GetBooks(Author author);
        Task Remove(Author author);
        Task Update(Author Author);
    }
}

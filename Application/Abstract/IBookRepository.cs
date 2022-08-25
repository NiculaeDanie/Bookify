using Bookify.Domain.Model;
using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book> GetById(int id);
        Task Add(Book book);
        Task<List<Book>> GetBookByGenre(Genre genre);
        Task<IFormFile> GetBookContent(Book book);
        Task Remove(Book product);
        Task Update(Book book);
    }
}

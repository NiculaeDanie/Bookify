using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        public IAuthorRepository AuthorRepository { get; }
        public IUserRepository UserRepository { get; }
        public IBookRepository BookRepository { get; }
        public IGenreRepository GenreRepository { get; }
        Task Save();
    }
}

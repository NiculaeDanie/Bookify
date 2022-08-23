using Application.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public IUserRepository UserRepository { get; private set; }
        public IBookRepository BookRepository { get; private set; }
        public IAuthorRepository AuthorRepository { get; private set; }
        public IGenreRepository GenreRepository { get; private set; }
        public UnitOfWork(DataContext dataContext, IUserRepository userRepository, IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _dataContext = dataContext;
            UserRepository = userRepository;
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;
            GenreRepository = genreRepository;
        }
        public async Task Save()
        {
            await _dataContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}

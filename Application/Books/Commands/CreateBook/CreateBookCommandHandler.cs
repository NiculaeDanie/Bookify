using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler: IRequestHandler<CreateBookCommand,int>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public CreateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            List<Author> authorList = new List<Author>();
            foreach(var id in request.authorId)
            {
                authorList.Add(_authorRepository.GetAuthor(id));
            }
            var book = new Book(request.title, authorList, request.releaseDate, request.description, request.genre, request.content);
            _bookRepository.CreateBook(book);
            return Task.FromResult(book.id);

        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.AddBookToAuthor
{
    public class AddBookToAuthorCommandHandler: IRequestHandler<AddBookToAuthorCommand,int>
    {
        private readonly IAuthorRepository _repository;
        private readonly IBookRepository _bookRepository;
        public AddBookToAuthorCommandHandler(IAuthorRepository repository, IBookRepository bookRepository)
        {
            _repository = repository;
            _bookRepository = bookRepository;
        }
        public Task<int> Handle(AddBookToAuthorCommand command, CancellationToken cancellationToken)
        {
            _repository.AddBookToAuthor(command.Id,_bookRepository.GetBook(command.bookId));

            return Task.FromResult(command.bookId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.AddBookToAuthor
{
    public class AddBookToAuthorCommandHandler
    {
        private readonly IAuthorRepository _repository;
        public AddBookToAuthorCommandHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }
        public Task<int> Handle(AddBookToAuthorCommand command, CancellationToken cancellationToken)
        {
            _repository.AddBookToAuthor(command.Id,command.book);

            return Task.FromResult(command.book.id);
        }
    }
}

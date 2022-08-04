using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IAuthorRepository _repository;
        public CreateAuthorCommandHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }
        public Task<int> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            var author = new Author(command.name,command.description);
            _repository.CreateAuthor(author);

            return Task.FromResult(author.id);
        }
    }
}

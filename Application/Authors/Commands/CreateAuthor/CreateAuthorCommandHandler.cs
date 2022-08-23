using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler: IRequestHandler<CreateAuthorCommand,Author>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var Author = new Author
            {
                Name = request.Name,
                Description = request.Description
            };
            await _unitOfWork.AuthorRepository.Add(Author);
            await _unitOfWork.Save();
            return Author;
        }
    }
}

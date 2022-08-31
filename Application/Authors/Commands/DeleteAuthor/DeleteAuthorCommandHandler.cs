using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler: IRequestHandler<DeleteAuthorCommand,Author>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetById(request.AuthorId);
            if (author == null) return null;

            _unitOfWork.AuthorRepository.Remove(author);
            await _unitOfWork.Save();

            return author;
        }
    }
}

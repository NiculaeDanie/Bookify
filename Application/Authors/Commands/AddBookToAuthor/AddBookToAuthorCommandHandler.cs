using Application.Abstract;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.AddBookToAuthor
{
    public class AddBookToAuthorCommandHandler: IRequestHandler<AddBookToAuthorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddBookToAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddBookToAuthorCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.AuthorRepository.AddBookToAuthor(request.Author, request.Book);
            await _unitOfWork.Save();
            return new Unit();
        }
    }
}

using Application.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.PublishBook
{
    public class PublishBookCommandHandler: IRequestHandler<PublishBookCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public PublishBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(PublishBookCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookRepository.PublishBook(request.Book);
            return new Unit();
        }
    }
}

using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler: IRequestHandler<GetBookByIdQuery,Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBookByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BookRepository.GetById(request.BookId);
        }
    }
}

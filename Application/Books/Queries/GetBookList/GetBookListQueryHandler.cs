using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookList
{
    public class GetBookListQueryHandler: IRequestHandler<GetBookListQuery,List<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBookListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BookRepository.GetAll();
        }
    }
}

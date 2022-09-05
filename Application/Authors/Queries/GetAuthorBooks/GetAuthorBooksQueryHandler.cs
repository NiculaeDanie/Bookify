using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Queries.GetAuthorBooks
{
    public class GetAuthorBooksQueryHandler: IRequestHandler<GetAuthorBooksQuery,List<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAuthorBooksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> Handle(GetAuthorBooksQuery request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetById(request.AuthorId);
            if (author == null)
            {
                return null;
            }
            return await _unitOfWork.BookRepository.GetBookByAuthor(author);
        }
    }
}

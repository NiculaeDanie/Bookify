using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.Search
{
    public class SearchQueryHandler: IRequestHandler<SearchQuery,List<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SearchQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BookRepository.Search(request.SString);
        }
    }
}

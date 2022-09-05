using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Queries.SearchAuthor
{
    public class SearchAuthorQueryHandler: IRequestHandler<SearchAuthorQuery,List<Author>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SearchAuthorQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Author>> Handle(SearchAuthorQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AuthorRepository.Search(request.Search);
        }
    }
}

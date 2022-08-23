using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Queries.GetAuthorList
{
    public class GetAuthorListQueryHandler: IRequestHandler<GetAuthorListQuery,List<Author>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAuthorListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Author>> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AuthorRepository.GetAll();
        }
    }
}

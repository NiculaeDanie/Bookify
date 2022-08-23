using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler: IRequestHandler<GetAuthorByIdQuery,Author>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AuthorRepository.GetById(request.Id);
        }
    }
}

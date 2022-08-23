﻿using Application.Abstract;
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
            return await _unitOfWork.AuthorRepository.GetBooks(request.Author);
        }
    }
}
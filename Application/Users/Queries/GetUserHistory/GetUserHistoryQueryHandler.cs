using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserHistory
{
    public class GetUserHistoryQueryHandler : IRequestHandler<GetUserHistoryQuery, List<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserHistoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> Handle(GetUserHistoryQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetById(request.UserId);
            if (user == null)
                return null;
            return await _unitOfWork.UserRepository.GetUserHistory(request.UserId);
        }
    }
}

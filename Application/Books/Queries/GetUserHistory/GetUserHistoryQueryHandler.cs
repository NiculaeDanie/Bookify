using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> userManager;
        public GetUserHistoryQueryHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<List<Book>> Handle(GetUserHistoryQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.UserId);
            if (user == null)
                return null;
            return await _unitOfWork.BookRepository.GetHistory(user.Id);
        }
    }
}

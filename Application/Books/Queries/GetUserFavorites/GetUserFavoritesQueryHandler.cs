using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserFavorites
{
    public class GetUserFavoritesQueryHandler: IRequestHandler<GetUserFavoritesQuery,List<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public GetUserFavoritesQueryHandler(IUnitOfWork unitOfWork, UserManager<User> userManager) { _unitOfWork = unitOfWork; _userManager = userManager; }

        public async Task<List<Book>> Handle(GetUserFavoritesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserId);
            if (user == null)
                return null;
            return await _unitOfWork.BookRepository.GetFavorites(user.Id);
        }
    }
}

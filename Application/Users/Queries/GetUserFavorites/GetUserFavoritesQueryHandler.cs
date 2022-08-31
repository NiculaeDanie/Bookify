using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
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
        public GetUserFavoritesQueryHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<List<Book>> Handle(GetUserFavoritesQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetById(request.UserId);
            if (user == null)
                return null;
            return await _unitOfWork.UserRepository.GetUserFavorites(request.UserId);
        }
    }
}

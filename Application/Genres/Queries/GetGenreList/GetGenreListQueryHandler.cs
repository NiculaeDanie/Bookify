using Application.Abstract;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Genres.Queries.GetGenreList
{
    public class GetGenreListQueryHandler : IRequestHandler<GetGenreListQuery,List<Genre>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetGenreListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Genre>> Handle(GetGenreListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GenreRepository.GetAll();
        }
    }
}

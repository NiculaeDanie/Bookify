using Application.Abstract;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Genres.Queries.GetGenreById
{
    public class GetGenreByIdQueyHandler: IRequestHandler<GetGenreByIdQuery,Genre>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetGenreByIdQueyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Genre> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GenreRepository.GetById(request.Id);

        }
    }
}

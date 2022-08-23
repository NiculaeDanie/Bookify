using Application.Abstract;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Genres.Commands.CreateGenre
{
    public class CreateGenreCommandHandler: IRequestHandler<CreateGenreCommand, Genre>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateGenreCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Genre> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = new Genre
            {
                Name= request.Genre
            };
            await _unitOfWork.GenreRepository.Add(genre);
            return genre;
        }
    }
}

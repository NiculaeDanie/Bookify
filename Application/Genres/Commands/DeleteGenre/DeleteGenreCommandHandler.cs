using Application.Abstract;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Genres.Commands.DeleteGenre
{
    internal class DeleteGenreCommandHandler: IRequestHandler<DeleteGenreCommand,Genre>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteGenreCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Genre> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(request.GenreId);
            if (genre == null) return null;

            _unitOfWork.GenreRepository.Remove(genre);
            await _unitOfWork.Save();

            return genre;
        }
    }
}

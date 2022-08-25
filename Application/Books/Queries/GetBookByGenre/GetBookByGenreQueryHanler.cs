using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookByGenre
{
    public class GetBookByGenreQueryHanler: IRequestHandler<GetBookByGenreQuery,List<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBookByGenreQueryHanler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> Handle(GetBookByGenreQuery request, CancellationToken cancellationToken)
        {
            var Genre = await _unitOfWork.GenreRepository.GetById(request.GenreId);
            return await _unitOfWork.BookRepository.GetBookByGenre(Genre);
        }
    }
}

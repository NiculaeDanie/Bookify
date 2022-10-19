using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookByGenreFiltered
{
    public class GetBookByGenreFilteredQueryHandler:IRequestHandler<GetBookByGenreFilteredQuery,List<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBookByGenreFilteredQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public async Task<List<Book>> Handle(GetBookByGenreFilteredQuery request, CancellationToken cancellationToken)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(request.GenreId);
           
            return await _unitOfWork.BookRepository.GetBookByGenre(genre);
        }
    }
}

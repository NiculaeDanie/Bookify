using Application.Abstract;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Genres.Queries.GetStatistics
{
    public class GetStatisticsQueryHandler: IRequestHandler<GetStatisticsQuery,Dictionary<Genre,int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetStatisticsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Dictionary<Genre, int>> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            var response = new Dictionary<Genre, int>();
            var books = await _unitOfWork.BookRepository.GetFullHistory();
            foreach (var book in books)
            {
                foreach (var item in book.UserBook)
                {
                    foreach (var genre in book.BookGenre)
                    {
                        if (!response.ContainsKey(genre.Genre))
                            response[genre.Genre] = 0;
                        response[genre.Genre]++;
                    }
                }
            }
            return response;
        }
    }
}

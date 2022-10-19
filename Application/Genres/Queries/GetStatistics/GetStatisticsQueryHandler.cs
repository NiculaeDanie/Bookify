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
    public class GetStatisticsQueryHandler: IRequestHandler<GetStatisticsQuery,Dictionary<string,int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetStatisticsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Dictionary<string, int>> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            var response = new Dictionary<string, int>();
            var books = await _unitOfWork.BookRepository.GetFullHistory();
            foreach (var book in books)
            {
                foreach (var item in book.UserBook)
                {
                    foreach (var genre in book.BookGenre)
                    {
                        if (!response.ContainsKey(genre.Genre.Name))
                            response[genre.Genre.Name] = 0;
                        response[genre.Genre.Name]++;
                    }
                }
            }
            return response;
        }
    }
}

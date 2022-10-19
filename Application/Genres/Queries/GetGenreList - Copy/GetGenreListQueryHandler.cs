using Application.Abstract;
using Azure;
using Bookify.Domain.Model;
using Domain;
using iText.Layout.Borders;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Application.Genres.Queries.GetGenreList
{
    public class GetGenreListUserQueryHandler : IRequestHandler<GetGenreListUserQuery, List<Genre>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> userManager;
        public GetGenreListUserQueryHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<List<Genre>> Handle(GetGenreListUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.UserId);
            var books = await _unitOfWork.BookRepository.GetHistory(user.Id);
            var response = new Dictionary<int, int>();
            foreach (var book in books)
            {
                foreach (var item in book.UserBook)
                {
                    foreach (var genre in book.BookGenre)
                    {
                        if (!response.ContainsKey(genre.Genre.Id))
                            response[genre.Genre.Id] = 0;
                        response[genre.Genre.Id]++;
                    }
                }
            }
            var result = response.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value).Keys.ToList();
            var result2 = new List<Genre>();
            foreach(var item in result)
            {
                result2.Add(await _unitOfWork.GenreRepository.GetById(item));
            }
            var toAdd = await _unitOfWork.GenreRepository.GetAll();
            foreach(var item in toAdd)
            {
                if(result2.Any(x=> x.Id == item.Id))
                {
                    continue;
                }
                result2.Add(item);
            }
            return result2;
        }
    }
}

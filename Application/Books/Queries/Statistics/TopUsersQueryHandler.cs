using Application.Abstract;
using Azure;
using Bookify.Domain.Model;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.Statistics
{
    public class TopUsersQueryHandler: IRequestHandler<TopUsersQuery,List<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public TopUsersQueryHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<List<string>> Handle(TopUsersQuery request, CancellationToken cancellationToken)
        {

            var history = await _unitOfWork.BookRepository.GetFullHistory();
            var result = new Dictionary<string, int>();
            foreach(var item in history)
            {
                foreach(var book in item.UserBook)
                {
                    if (!result.ContainsKey(book.UserId))
                        result[book.UserId] = 0;
                    result[book.UserId]++;
                }
            }
            var response = result.OrderBy(x => x.Value).Take(10).ToDictionary(x => x.Key, x => x.Value).Keys.ToList();
            var response2= new List<string>();
            foreach(var item in response)
            {
                var user = await _userManager.FindByIdAsync(item);
                response2.Add(user.UserName);
            }
            return response2;
        }
    }
}

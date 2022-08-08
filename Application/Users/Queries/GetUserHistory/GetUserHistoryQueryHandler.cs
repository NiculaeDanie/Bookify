using Application.Books.Queries;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserHistory
{
    public class GetUserHistoryQueryHandler: IRequestHandler<GetUserHistoryQuery,IEnumerable<BookVm>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserHistoryQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<BookVm>> Handle(GetUserHistoryQuery request, CancellationToken cancellationToken)
        {
            var result = _userRepository.GetHistory(request.userId).Select(book => new BookVm
            {
                id = book.id,
                title = book.title,
                releaseDate = book.releaseDate,
                description = book.descriprion,
                status = book.status,
                genre = book.genre
            });
            return Task.FromResult(result);
        }
    }
}

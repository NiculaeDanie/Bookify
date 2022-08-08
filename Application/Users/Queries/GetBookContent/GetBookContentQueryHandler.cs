using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetBookContent
{
    public class GetBookContentQueryHandler: IRequestHandler<GetBookContentQuery,string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        public GetBookContentQueryHandler(IUserRepository userRepository, IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public Task<string> Handle(GetBookContentQuery request, CancellationToken cancellationToken)
        {
            var book = _bookRepository.GetBook(request.bookId);
            _userRepository.AddBookToHistory(request.userId, book);
            return Task.FromResult(_bookRepository.GetContent(request.bookId));
        }
    }
}

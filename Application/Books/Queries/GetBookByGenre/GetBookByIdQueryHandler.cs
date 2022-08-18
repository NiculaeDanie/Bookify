using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookByGenre
{
    public class GetBookByIdQueryHandler: IRequestHandler<GetBookByIdQuery,IEnumerable<Book>>
    {
        private readonly IBookRepository _bookRepository;
        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<IEnumerable<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var books = _bookRepository.GetAllBooks().Where(b => b.genre.Contains(request.genre));

            return Task.FromResult(books);
        }
    }
}

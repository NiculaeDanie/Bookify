using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler: IRequestHandler<GetBookByIdQuery,BookVm>
    {
        private readonly IBookRepository _bookRepository;
        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<BookVm> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = _bookRepository.GetBook(request.id);
            var result = new BookVm
            {
                    title = book.title,
                    releaseDate = book.releaseDate,
                    description = book.descriprion,
                    status = book.status,
                    genre = book.genre
            };
            return Task.FromResult(result);
        }
    }
}

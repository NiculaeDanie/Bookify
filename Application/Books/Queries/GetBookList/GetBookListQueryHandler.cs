using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookList
{
    public class GetBookListQueryHandler: IRequestHandler<GetBookListQuery,IEnumerable<BookVm>>
    {
        private readonly IBookRepository _bookRepository;
        public GetBookListQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<IEnumerable<BookVm>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
        {
            var result = _bookRepository.GetAllBooks().Select(book => new BookVm
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

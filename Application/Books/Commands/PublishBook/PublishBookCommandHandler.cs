using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.PublishBook
{
    public class PublishBookCommandHandler: IRequestHandler<PublishBookCommand,int>
    {
        private readonly IBookRepository _bookRepository;
        public PublishBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<int> Handle(PublishBookCommand request, CancellationToken cancellationToken)
        {
            var book = _bookRepository.GetBook(request.bookId);
            book.PublishBook();

            return Task.FromResult(book.id);
        }
    }
}

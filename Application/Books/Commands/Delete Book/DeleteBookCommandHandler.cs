using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.Delete_Book
{
    public class DeleteBookCommandHandler: IRequestHandler<DeleteBookCommand,Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetById(request.BookId);
            if (book == null) return null;

            _unitOfWork.BookRepository.Remove(book);
            await _unitOfWork.Save();

            return book;
        }
    }
}

using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Books.Commands.Delete_Book
{
    public class DeleteBookCommandHandler: IRequestHandler<DeleteBookCommand,Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureStorage azureStorage;
        public DeleteBookCommandHandler(IUnitOfWork unitOfWork, IAzureStorage azureStorage)
        {
            _unitOfWork = unitOfWork;
            this.azureStorage = azureStorage;
        }

        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetById(request.BookId);
            if (book == null) return null;
            await azureStorage.DeleteAsync(Regex.Replace(book.Title, @"\s+", "").ToLower() + ".pdf");
            await azureStorage.DeleteAsync(Regex.Replace(book.Title, @"\s+", "").ToLower() + ".jpg");
            _unitOfWork.BookRepository.Remove(book);
            await _unitOfWork.Save();

            return book;
        }
    }
}

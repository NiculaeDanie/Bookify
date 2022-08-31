using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.AddGenreToBook
{
    public class AddGenreToBookCommandHandler: IRequestHandler<AddGenreToBookCommand,Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddGenreToBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle(AddGenreToBookCommand request, CancellationToken cancellationToken)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(request.GenreId);
            var book = await _unitOfWork.BookRepository.GetById(request.BookId);
            if (book == null || genre == null)
            {
                return null;
            }
            var result = await _unitOfWork.BookRepository.AddGenreToBook(genre, book);
            await _unitOfWork.Save();
            return result;
        }
    }
}

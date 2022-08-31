using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.AddBookToFavorites
{
    public class AddBookToFavoritesCommandHandler: IRequestHandler<AddBookToFavoritesCommand,Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddBookToFavoritesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle(AddBookToFavoritesCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetById(request.BookId);
            var user = await _unitOfWork.UserRepository.GetById(request.UserId);
            if (book == null || user == null)
                return null;
            await _unitOfWork.UserRepository.AddBookToFavorites(book,user);
            await _unitOfWork.Save();
            return book;
        }
    }
}

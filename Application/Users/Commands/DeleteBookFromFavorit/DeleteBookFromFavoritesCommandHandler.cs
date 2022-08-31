using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.DeleteBookFromFavorit
{
    public class DeleteBookFromFavoritesCommandHandler: IRequestHandler<DeleteBookFromFavoritesCommand,Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBookFromFavoritesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle(DeleteBookFromFavoritesCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetById(request.BookId);
            var user = await _unitOfWork.UserRepository.GetById(request.UserId);
            if (user == null || book == null)
                return null;
            await _unitOfWork.UserRepository.DeleteFromFavorites(book, user);
            await _unitOfWork.Save();
            return book;
        }
    }
}

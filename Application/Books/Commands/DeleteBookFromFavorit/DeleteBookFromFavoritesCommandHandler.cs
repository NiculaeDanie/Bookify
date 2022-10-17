using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        public DeleteBookFromFavoritesCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Book> Handle(DeleteBookFromFavoritesCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetById(request.BookId);
            var user = await _userManager.FindByEmailAsync(request.UserId);
            if ( book == null)
                return null;
            var item = book.UserFavorites.Single(b=> b.UserId == user.Id);
            book.UserFavorites.Remove(item);
            await _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.Save();
            return book;
        }
    }
}

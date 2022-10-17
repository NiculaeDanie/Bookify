using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> userManager;
        public AddBookToFavoritesCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<Book> Handle(AddBookToFavoritesCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetById(request.BookId);
            var user = await userManager.FindByEmailAsync(request.UserId);
            if (book == null || user == null)
                return null;
            book.UserFavorites.Add(new Domain.UserFavorites() { 
                Book = book,
                User = user,
                BookId = book.Id,
                UserId = user.Id
                });
            await _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.Save();
            return book;
        }
    }
}

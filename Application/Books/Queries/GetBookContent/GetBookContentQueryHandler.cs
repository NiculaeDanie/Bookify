
using Application.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Bookify.Domain.Model;

namespace Application.Users.Queries.GetBookContent
{
    public class GetBookContentQueryHandler : IRequestHandler<GetBookContentQuery,BlobDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAzureStorage azureStorage;
        private readonly UserManager<User> _userManager;
        public GetBookContentQueryHandler(IUnitOfWork unitOfWork, IAzureStorage azureStorage, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.azureStorage = azureStorage;
            _userManager = userManager;
        }
        public async Task<BlobDto> Handle(GetBookContentQuery request, CancellationToken cancellationToken)
        {
            var book = await unitOfWork.BookRepository.GetById(request.BookId);
            var user = await _userManager.FindByEmailAsync(request.UserId);
            if(book == null || user == null)
            {
                return null;
            }
            book.UserBook.Add(new Domain.UserBook()
            {
                BookId = book.Id,
                UserId = user.Id,
                Book = book,
                User = user
            });
            await unitOfWork.BookRepository.Update(book);
            await unitOfWork.BookRepository.IncrementViewCount(book);
            await unitOfWork.Save();
            return await azureStorage.DownloadAsync(Regex.Replace(book.Title, @"\s+", "").ToLower() + ".pdf");
        }
    }
}

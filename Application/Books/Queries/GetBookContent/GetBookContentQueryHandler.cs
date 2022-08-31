
using Application.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;

namespace Application.Users.Queries.GetBookContent
{
    public class GetBookContentQueryHandler : IRequestHandler<GetBookContentQuery, byte[]>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetBookContentQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<byte[]> Handle(GetBookContentQuery request, CancellationToken cancellationToken)
        {
            var book = await unitOfWork.BookRepository.GetById(request.BookId);
            var user = await unitOfWork.UserRepository.GetById(request.UserId);
            if(book == null || user == null)
            {
                return null;
            }
            await unitOfWork.UserRepository.AddBookToHistory(book,user);
            await unitOfWork.BookRepository.IncrementViewCount(book);
            await unitOfWork.Save();
            return book.Content;
        }
    }
}

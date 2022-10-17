using Application.Abstract;
using Application.Users.Queries.GetBookContent;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookImage
{
    public class GetBookImageQueryHandler: IRequestHandler<GetBookImageQuery,BlobDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAzureStorage azureStorage;
        public GetBookImageQueryHandler(IUnitOfWork unitOfWork, IAzureStorage azureStorage)
        {
            this.unitOfWork = unitOfWork;
            this.azureStorage = azureStorage;
        }
        public async Task<BlobDto> Handle(GetBookImageQuery request, CancellationToken cancellationToken)
        {
            var book = await unitOfWork.BookRepository.GetById(request.BookId);
            if (book == null)
            {
                return null;
            }
            return await azureStorage.DownloadAsync(Regex.Replace(book.Title, @"\s+", "").ToLower() + ".jpg");
        }
    }
}

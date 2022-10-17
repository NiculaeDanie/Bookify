using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler: IRequestHandler<UpdateBookCommand,Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureStorage azureStorage;
        public UpdateBookCommandHandler(IUnitOfWork unitOfWork, IAzureStorage azureStorage)
        {
            _unitOfWork = unitOfWork;
            this.azureStorage = azureStorage;
        }

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var toUpdate = new Book();
            toUpdate.Title = request.Title;
            toUpdate.Description = request.Description;
            toUpdate.Id = request.BookId;
            toUpdate.ReleaseDate = request.Created;
            if(request.Content != null)
            {
                await azureStorage.DeleteAsync(Regex.Replace(request.Title, @"\s+", "").ToLower() + ".pdf");
                await azureStorage.UploadAsync(request.Content, Regex.Replace(request.Title, @"\s+", "").ToLower() + ".pdf");
            }
            if(request.Image != null)
            {
                await azureStorage.DeleteAsync(Regex.Replace(request.Title, @"\s+", "").ToLower() + ".jpg");
                await azureStorage.UploadAsync(request.Image, Regex.Replace(request.Title, @"\s+", "").ToLower() + ".jpg");
            }

            await _unitOfWork.BookRepository.Update(toUpdate);
            await _unitOfWork.Save();

            return toUpdate;
        }
    }
}

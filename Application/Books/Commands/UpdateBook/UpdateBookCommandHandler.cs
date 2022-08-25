using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler: IRequestHandler<UpdateBookCommand,Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var toUpdate = new Book();
            toUpdate.Title = request.Title;
            toUpdate.Description = request.Description;
            toUpdate.Id = request.BookId;
            await using var memoryStream = new MemoryStream();
            await request.Content.CopyToAsync(memoryStream);
            var content = memoryStream.ToArray();
            toUpdate.Content = content;
            toUpdate.Status = (Status)0;
            toUpdate.ReleaseDate = request.Created;


            await _unitOfWork.BookRepository.Update(toUpdate);
            await _unitOfWork.Save();

            return toUpdate;
        }
    }
}

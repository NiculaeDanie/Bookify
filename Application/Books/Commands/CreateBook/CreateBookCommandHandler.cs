using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler: IRequestHandler<CreateBookCommand,Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            await request.Content.CopyToAsync(memoryStream);
            var content = memoryStream.ToArray();
            var Book = new Book
            {
                Title = request.Title,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate,
                Status= 0,
                ViewCount=0,
                Content= content,
                ImageUrl = request.ImageUrl
            };
            await _unitOfWork.BookRepository.Add(Book);
            await _unitOfWork.Save();
            return Book;
        }
    }
}

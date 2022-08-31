using Application.Abstract;
using Bookify.Domain.Model;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.AddBookToAuthor
{
    public class AddBookToAuthorCommandHandler: IRequestHandler<AddBookToAuthorCommand,Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddBookToAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle(AddBookToAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetById(request.AuthorId);
            var book = await _unitOfWork.BookRepository.GetById(request.BookId);
            if(author == null || book == null)
            {
                return null;
            }
            await _unitOfWork.AuthorRepository.AddBookToAuthor(author, book);
            await _unitOfWork.Save();
            return book;
        }
    }
}

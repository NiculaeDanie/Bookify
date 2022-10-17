using Bookify.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand: IRequest<Book>
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public IFormFile? Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}

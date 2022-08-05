using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommand: IRequest<int>
    {
        public List<int> authorId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime releaseDate { get; set; }
        public List<String> genre { get; set; }
        public string content { get; set; }
    }
}

using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.PublishBook
{
    public class PublishBookCommand: IRequest
    {
        public Book Book { get; set; }
    }
}

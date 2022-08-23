using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookByIdQuery: IRequest<Book>
    {
        public int BookId { get; set; }
    }
}

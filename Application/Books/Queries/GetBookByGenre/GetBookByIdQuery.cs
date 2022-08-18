using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookByGenre
{
    public class GetBookByIdQuery: IRequest<IEnumerable<Book>>
    {
        public string genre { get; set; }
    }
}

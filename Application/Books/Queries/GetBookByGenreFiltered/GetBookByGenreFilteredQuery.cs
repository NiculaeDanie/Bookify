using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookByGenreFiltered
{
    public class GetBookByGenreFilteredQuery: IRequest<List<Book>>
    {
        public int GenreId { get; set; }
        public int UserId { get; set; }
    }
}

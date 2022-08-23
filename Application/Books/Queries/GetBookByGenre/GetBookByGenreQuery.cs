using Bookify.Domain.Model;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookByGenre
{
    public class GetBookByGenreQuery: IRequest<List<Book>>
    {
        public Genre Genre { get; set; }
    }
}

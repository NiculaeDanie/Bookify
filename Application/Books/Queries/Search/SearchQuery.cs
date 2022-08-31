using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.Search
{
    public class SearchQuery: IRequest<List<Book>>
    {
        public string SString { get; set; }
    }
}

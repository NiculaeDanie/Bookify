using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Queries.GetAuthorBooks
{
    public class GetAuthorBooksQuery: IRequest<List<Bookify.Domain.Model.Book>>
    {
        public int AuthorId { get; set; }
    }
}

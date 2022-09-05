
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Queries.SearchAuthor
{
    public class SearchAuthorQuery: IRequest<List<Author>>
    {
        public string Search { get; set; }
    }
}

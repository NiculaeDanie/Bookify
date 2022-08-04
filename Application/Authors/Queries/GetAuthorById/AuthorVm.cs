using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Queries.GetAuthorById
{
    public class AuthorVm
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<BookListDto> books { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetBookContent
{
    public class GetBookContentQuery: IRequest<string>
    {
        public int userId { get; set; }
        public int bookId { get; set; }
    }
}

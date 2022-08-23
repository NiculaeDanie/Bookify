
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetBookContent
{
    public class GetBookContentQuery: IRequest<IFormFile>
    {
        public int BookId { get; set; }
    }
}

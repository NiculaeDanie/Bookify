
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetBookContent
{
    public class GetBookContentQueryHandler : IRequestHandler<GetBookContentQuery, IFormFile>
    {
        public Task<IFormFile> Handle(GetBookContentQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

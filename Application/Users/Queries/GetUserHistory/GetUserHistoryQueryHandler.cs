using Bookify.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserHistory
{
    public class GetUserHistoryQueryHandler : IRequestHandler<GetUserHistoryQuery, List<Book>>
    {
        public Task<List<Book>> Handle(GetUserHistoryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

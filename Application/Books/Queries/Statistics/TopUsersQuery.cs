using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.Statistics
{
    public class TopUsersQuery: IRequest<List<string>>
    {
    }
}

using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserPreferences
{
    public class GetUserPreferencesQuery: IRequest<List<string>>
    {
        public int UserId { get; set; }
    }
}

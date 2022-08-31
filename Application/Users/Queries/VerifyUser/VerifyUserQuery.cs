using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.VerifyUser
{
    public class VerifyUserQuery: IRequest<User>
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}

using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserFavorites
{
    public class GetUserFavoritesQuery: IRequest<List<Book>>
    {
        public string UserId { get; set; }
    }
}

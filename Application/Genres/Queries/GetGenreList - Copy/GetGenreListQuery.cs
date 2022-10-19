using Bookify.Domain.Model;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Genres.Queries.GetGenreList
{
    public class GetGenreListUserQuery: IRequest<List<Genre>>
    {
        public string UserId { get; set; }
    }
}

using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Genres.Queries.GetGenreById
{
    public class GetGenreByIdQuery: IRequest<Genre>
    {
        public int Id { get; set; }
    }
}

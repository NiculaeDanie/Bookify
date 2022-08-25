using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Genres.Commands.DeleteGenre
{
    public class DeleteGenreCommand: IRequest<Genre>
    {
        public int GenreId { get; set; }
    }
}

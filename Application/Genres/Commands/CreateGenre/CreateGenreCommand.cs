using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Genres.Commands.CreateGenre
{
    public class CreateGenreCommand: IRequest<Genre>
    {
        public string Genre { get; set; }
    }
}

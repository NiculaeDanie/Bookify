using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.DeleteBookFromFavorit
{
    public class DeleteBookFromFavoritesCommand: IRequest<Book>
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
    }
}

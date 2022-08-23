using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.AddBookToHistory
{
    public class AddBookToHistoryCommand: IRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}

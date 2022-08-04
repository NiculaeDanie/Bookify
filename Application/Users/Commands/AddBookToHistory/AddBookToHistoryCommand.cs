using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.AddBookToHistory
{
    public class AddBookToHistoryCommand: IRequest<int>
    {
        public int Id { get; set; }
        public Book book { get; set; }
    }
}

using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand: IRequest<Author>
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

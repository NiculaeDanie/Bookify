using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand: IRequest<Author>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

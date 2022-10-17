
using Application.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetBookContent
{
    public class GetBookContentQuery: IRequest<BlobDto>
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
    }
}

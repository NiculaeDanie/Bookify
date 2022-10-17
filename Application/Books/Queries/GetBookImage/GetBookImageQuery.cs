using Application.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBookImage
{
    public class GetBookImageQuery: IRequest<BlobDto>
    {
        public int BookId { get; set; }
    }
}

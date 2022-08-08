﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.PublishBook
{
    public class PublishBookCommand: IRequest<int>
    {
        public int bookId { get; set; }
    }
}

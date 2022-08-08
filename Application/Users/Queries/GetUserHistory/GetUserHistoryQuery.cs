﻿using Application.Books.Queries;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserHistory
{
    public class GetUserHistoryQuery: IRequest<IEnumerable<BookVm>>
    {
        public int userId { get; set; }
    }
}

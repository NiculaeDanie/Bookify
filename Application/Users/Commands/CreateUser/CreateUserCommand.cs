﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand: IRequest<int>
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
    }
}

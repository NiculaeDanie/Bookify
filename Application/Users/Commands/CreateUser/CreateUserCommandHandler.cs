using Application.Abstract;
using Bookify.Domain.Model;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _unitOfWork;
        public CreateUserCommandHandler(IUserRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var ex = await _unitOfWork.GetByName(request.Name);
            if (ex != null)
            {
                return null;
            }
            var user = new User
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Name
            };
            await _unitOfWork.CreateUser(user,request.Password);
            return user;
        }
    }
}

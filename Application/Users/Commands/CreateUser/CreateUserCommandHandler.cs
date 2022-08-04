using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler: IRequestHandler<CreateUserCommand,int>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User(command.email,command.password,command.name);
            _userRepository.CreateUser(user);

            return Task.FromResult(user.id);
        }
    }
}

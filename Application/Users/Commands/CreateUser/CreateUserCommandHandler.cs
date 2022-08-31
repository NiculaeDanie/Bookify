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
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var ex = await _unitOfWork.UserRepository.VerifyEmail(request.Email);
            if (ex)
            {
                return null;
            }
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };
            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.Save();
            return user;
        }
    }
}

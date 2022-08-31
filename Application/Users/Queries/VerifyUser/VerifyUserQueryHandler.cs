using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.VerifyUser
{
    public class VerifyUserQueryHandler: IRequestHandler<VerifyUserQuery,User>
    {
        private readonly IUnitOfWork _unitOfWork;
        public VerifyUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(VerifyUserQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.VerifyUser(request.email, request.password);
        }
    }
}

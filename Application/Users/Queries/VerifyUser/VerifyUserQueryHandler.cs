using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.VerifyUser
{
    public class VerifyUserQueryHandler: IRequestHandler<VerifyUserQuery,int>
    {
        private readonly IUserRepository _userRepository;
        public VerifyUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<int> Handle(VerifyUserQuery request, CancellationToken cancellationToken)
        {
            var result = _userRepository.VerifyUser(request.email, request.password);

            return Task.FromResult(result);
        }
    }
}

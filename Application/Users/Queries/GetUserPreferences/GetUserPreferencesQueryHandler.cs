using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserPreferences
{
    public class GetUserPreferencesQueryHandler: IRequestHandler<GetUserPreferencesQuery, List<string>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserPreferencesQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<string>> Handle(GetUserPreferencesQuery request, CancellationToken cancellationToken)
        {
            var history = _userRepository.GetUser(request.UserId).GetUserPreferences();
            
            return Task.FromResult(history);
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.AddBookToHistory
{
    public class AddBookToHistoryCommandHandler: IRequestHandler<AddBookToHistoryCommand,int>
    {
        private readonly IUserRepository _userRepository;
        public AddBookToHistoryCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<int> Handle(AddBookToHistoryCommand request, CancellationToken cancellationToken)
        {
            _userRepository.AddBookToHistory(request.Id, request.book);

            return Task.FromResult(request.Id);
        }
    }
}

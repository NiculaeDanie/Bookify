using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler: IRequestHandler<UpdateAuthorCommand,Author>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var toUpdate = new Author();
            toUpdate.Name = request.Name;
            toUpdate.Description = request.Description;
            toUpdate.Id = request.AuthorId;
            


            await _unitOfWork.AuthorRepository.Update(toUpdate);
            await _unitOfWork.Save();

            return toUpdate;
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler: IRequestHandler<GetAuthorByIdQuery,AuthorVm>
    {
        private readonly IAuthorRepository _repository;
        public GetAuthorByIdQueryHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public Task<AuthorVm> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = _repository.GetAuthor(request.id);
            var result =new AuthorVm
            {
                Id = author.id,
                name = author.name,
                description = author.description,
                books = author.books.Select(item => new BookListDto
                {
                    title = item.title,
                    date = item.releaseDate,
                    description = item.descriprion,
                    status = item.status,
                    genre = item.genre
                }).ToList()
            };
            return Task.FromResult(result);
        }
    }
}

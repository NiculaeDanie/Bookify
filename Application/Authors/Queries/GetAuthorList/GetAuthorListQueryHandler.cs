using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Queries.GetAuthorList
{
    public class GetAuthorListQueryHandler: IRequestHandler<GetAuthorListQuery,IEnumerable<AuthorListVm>>
    {
        private readonly IAuthorRepository _repository;
        public GetAuthorListQueryHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<AuthorListVm>> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
        {
            var result = _repository.GetAuthors().Select(author => new AuthorListVm
            {
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
            }) ;
            return Task.FromResult(result);
        }
    }
}

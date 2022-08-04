using Application;
using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Queries.GetAuthorList;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var diContainer = new ServiceCollection()
                .AddMediatR(typeof(IBookRepository))
                .AddMediatR(typeof(IAuthorRepository))
                .AddMediatR(typeof(IUserRepository))
                .AddScoped<IAuthorRepository, InMemoryAuthorRepository>()
                .AddScoped<IBookRepository, InMemoryBookRepository>()
                .AddScoped<IUserRepository,InMemoryUserRepository>()
                .BuildServiceProvider();

var mediator = diContainer.GetRequiredService<IMediator>();

var authorId = await mediator.Send(new CreateAuthorCommand
{
    name = "Niculae Daniel",
    description = "test"
});

Console.WriteLine($"Author created with id {authorId}");

var authors = await mediator.Send(new GetAuthorListQuery());
foreach(var author in authors)
{
    Console.WriteLine(author.name);
}
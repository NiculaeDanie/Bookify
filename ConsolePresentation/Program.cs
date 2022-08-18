using Application;
using Application.Authors.Commands.AddBookToAuthor;
using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Queries.GetAuthorList;
using Application.Books.Commands.CreateBook;
using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetBookContent;
using Application.Users.Queries.GetUserHistory;
using Application.Users.Queries.GetUserPreferences;
using Application.Users.Queries.VerifyUser;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var diContainer = new ServiceCollection()
                .AddMediatR(typeof(IBookRepository).GetTypeInfo().Assembly)
                .AddMediatR(typeof(IAuthorRepository).GetTypeInfo().Assembly)
                .AddMediatR(typeof(IUserRepository).GetTypeInfo().Assembly)
                .AddScoped<IAuthorRepository, InMemoryAuthorRepository>()
                .AddScoped<IBookRepository, InMemoryBookRepository>()
                .AddScoped<IUserRepository,InMemoryUserRepository>()
                .BuildServiceProvider();

var mediator = diContainer.GetRequiredService<IMediator>();
List<int> authorsId = new List<int>();
var authorId = await mediator.Send(new CreateAuthorCommand
{
    name = "Niculae Daniel",
    description = "test"
});
authorsId.Add(authorId);
Console.WriteLine($"Author created with id {authorId}");
var bookId = await mediator.Send(new CreateBookCommand
{
    authorId = authorsId,
    title = "test",
    description = "test",
    releaseDate = DateTime.Now,
    genre = new List<string>() {"Mistery", "Drama"},
    content = "test"
});
Console.WriteLine($"Book created with id {bookId}");
await mediator.Send(new AddBookToAuthorCommand
{
    Id = authorId,
    bookId = bookId
});

var authors = await mediator.Send(new GetAuthorListQuery());
foreach(var author in authors)
{
    Console.WriteLine(author.name);
}

await mediator.Send(new CreateUserCommand
{
    email = "test",
    password = "test",
    name = "test"
});

var user = await mediator.Send(new VerifyUserQuery
{
    email = "test",
    password = "test"
});

var bookContent=await mediator.Send(new GetBookContentQuery
{
    userId = user,
    bookId = bookId
});
Console.WriteLine(bookContent);
var userHistory = await mediator.Send(new GetUserPreferencesQuery
{
    UserId = user
});
Console.WriteLine("User history");
foreach (var book in userHistory)
{
    Console.WriteLine(book);
}

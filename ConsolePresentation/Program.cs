
using Application;
using Application.Abstract;
using Application.Authors.Commands.CreateAuthor;
using Application.Books.Commands.CreateBook;
using Infrastructure;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Application.Books.Queries.GetBookList;
using System.Reflection;
using Application.Authors.Commands.AddBookToAuthor;
using Application.Authors.Queries.GetAuthorBooks;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using Bookify.Domain.Model;
using Application.Authors.Queries.GetAuthorById;
using Application.Books.Queries.GetBookById;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var context = new DataContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var mediator = Init();
        while (true)
        {
            Console.WriteLine("Choose an option");
            Console.WriteLine("1 Add book");
            Console.WriteLine("2 Get book by id");
            Console.WriteLine("3 Add author");
            Console.WriteLine("4 Get author by id");
            Console.WriteLine("5 Add author to book");
            Console.WriteLine("6 Display Books by author");

            var action = Convert.ToInt32(Console.ReadLine());

            switch (action)
            {
                case 1:
                    var addedBook = await AddBook(mediator);
                    DisplayItem<Book>(addedBook);
                    break;
                case 2:
                    var book = await GetBookById(mediator);
                    DisplayItem<Book>(book);
                    break;
                case 3:
                    var addedAuthor = await AddAuthor(mediator);
                    DisplayItem<Author>(addedAuthor);
                    break;
                case 4:
                    var author = await GetAuthorById(mediator);
                    DisplayItem<Author>(author);
                    break;
                case 5:
                    await AddAuthorToBook(mediator);
                    break;
                case 6:
                    var books = await GetBooksByAuthorId(mediator);
                    foreach(var a in books)
                    {
                        DisplayItem<Book>(a);
                    }
                    break;
                default:
                    Console.WriteLine($"Invalid action: {action}");
                    break;
            }
        }
    }
    private static IMediator Init()
    {
        var diContainer = new ServiceCollection()
                .AddDbContext<DataContext>(options =>
                        options.UseSqlServer(@"Data Source=DESKTOP-MHJDP0S\SQLEXPRESS;Initial Catalog=Bookify;Trusted_Connection=True;"))
                .AddMediatR(typeof(AssemblyMarker))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IAuthorRepository, AuthorRepository>()
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<IGenreRepository, GenreRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .BuildServiceProvider();

        return diContainer.GetRequiredService<IMediator>();
    }
    private static void DisplayItem<T>(T item)
    {
        var serializedProduct = JsonConvert.SerializeObject(item, new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            Formatting = Formatting.Indented,
        });

        Console.WriteLine(serializedProduct);
        Console.WriteLine();
    }
    static async Task<Author> GetAuthorById(IMediator mediator)
    {
        var GetAuthorQuery = new GetAuthorByIdQuery();
        Console.WriteLine($"Insert {nameof(GetAuthorQuery.Id)}");
        GetAuthorQuery.Id = Convert.ToInt32(Console.ReadLine());

        return await mediator.Send(GetAuthorQuery);
    }
    static async Task<Book> GetBookById(IMediator mediator)
    {
        var GetBookQuery = new GetBookByIdQuery();
        Console.WriteLine($"Insert {nameof(GetBookQuery.BookId)}");
        GetBookQuery.BookId = Convert.ToInt32(Console.ReadLine());

        return await mediator.Send(GetBookQuery);
    }
    static async Task<Book> AddBook(IMediator mediator)
    {
        var addBookCommand = new CreateBookCommand();

        Console.WriteLine($"Insert {nameof(addBookCommand.Title)}:");
        addBookCommand.Title = Console.ReadLine();

        Console.WriteLine($"Insert {nameof(addBookCommand.Description)}:");
        addBookCommand.Description = Console.ReadLine();

        Console.WriteLine($"Insert {nameof(addBookCommand.ReleaseDate)}:");
        var dateInput = Console.ReadLine();
        var date = DateTime.Parse(dateInput);
        addBookCommand.ReleaseDate = date;

        var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");
        addBookCommand.Content = file;

        return await mediator.Send(addBookCommand);
    }
    static async Task<Author> AddAuthor(IMediator mediator)
    {
        var createAuthorCommand = new CreateAuthorCommand();

        Console.WriteLine($"Insert {nameof(createAuthorCommand.Name)}:");
        createAuthorCommand.Name = Console.ReadLine();

        Console.WriteLine($"Insert {nameof(createAuthorCommand.Description)}:");
        createAuthorCommand.Description = Console.ReadLine();

        return await mediator.Send(createAuthorCommand);
    }
    static async Task AddAuthorToBook(IMediator mediator)
    {
        var addAuthorToBookCommand = new AddBookToAuthorCommand();
        Console.WriteLine("Select book id");
        var input = Console.ReadLine();
        addAuthorToBookCommand.Book = await mediator.Send(new GetBookByIdQuery
        {
            BookId = Int32.Parse(input)
        });

        Console.WriteLine("Select author id");
        var inp = Console.ReadLine();
        addAuthorToBookCommand.Author = await mediator.Send(new GetAuthorByIdQuery
        {
            Id= Int32.Parse(inp)
        });

        await mediator.Send(addAuthorToBookCommand);
    }
    static async Task<List<Book>> GetBooksByAuthorId(IMediator mediator)
    {
        var getBooksByAuthorQuery = new GetAuthorBooksQuery();
        Console.WriteLine("Select author id");
        var inp = Console.ReadLine();
        getBooksByAuthorQuery.Author = await mediator.Send(new GetAuthorByIdQuery
        {
            Id = Int32.Parse(inp)
        });
        return await mediator.Send(getBooksByAuthorQuery);
    }
}
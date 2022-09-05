
using Application;
using Application.Abstract;
using Application.Authors.Commands.CreateAuthor;
using Application.Books.Commands.CreateBook;
using Infrastructure;
using Infrastructure.Repository;
using MediatR;
 
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
using ConsolePresentation;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var context = new DataContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        Seeder.SeedData(context);
    }
    
}
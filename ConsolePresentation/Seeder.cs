using Bogus;
using Bookify.Domain.Model;
using Infrastructure;
 
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using Domain;
using static Bogus.DataSets.Name;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.EntityFrameworkCore;

namespace ConsolePresentation
{
    public class Seeder
    {
        public static async void SeedData(DataContext context)
        {
            

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            
            var authors = GetPreconfiguredAuthors();
            var Genres = GetPreconfiguredGenres();
            var books = await GetPreconfiguredBooksAsync(authors, Genres);


            context.Authors.AddRange(authors);
            context.Genres.AddRange(Genres);
            context.Books.AddRange(books);

            context.SaveChanges();

            books = Update(context.Books.Include(Book=>Book.BookGenre).Include(book=>book.AuthorBook).ToList(), context.Genres.ToList(), context.Authors.ToList());
            context.Books.UpdateRange(books);
            context.SaveChanges();

        }
        private static List<Book> Update(List<Book> books , List<Genre> genres , List <Author> authors)
        {
            foreach (var book in books)
            {
                var a = new BookGenre() { Book = book, BookId = book.Id, Genre = genres.FirstOrDefault(), GenreId = genres.Last().Id };
                book.BookGenre.Add(a);
                book.AuthorBook.Add(new AuthorBook { Book = book, BookId = book.Id, Author = authors.First(), AuthorId = authors.First().Id });
            }
            return books;
        }
        private static List<Genre> GetPreconfiguredGenres()
        {
            string[] authorNames =
            {
                "Fantasy",
                "History",
                "Comedy",
                "Mistery",
                "Romance",
                "Action",
                "Police",
                "Thriller",
                "Drama",
                "Horror"
            };

            return authorNames.ToList().Select(bookName =>
                new Faker<Genre>()
                    .RuleFor(book => book.Name, bookName)
                    .Generate()).ToList();
        }

        private static List<Author> GetPreconfiguredAuthors()
        {
            string[] authorNames =
            {
                "Isadora Lang",
                "Tala Holt",
                "Leja Dickson",
                "Stan Sanchez",
                "Sumaiyah Brook",
                "Malachy Monroe",
                "Hajra Rojas",
                "Jada Carney",
                "Kiya Rowley",
                "Philip Santos"
        };

            var publisherNames = Enumerable.Range(1, 3)
                .Select(_ => new Faker().Company.CompanyName())
                .ToList();


            return authorNames.ToList().Select(bookName =>
                new Faker<Author>()
                    .RuleFor(book => book.Name, bookName)
                    .RuleFor(book => book.Description, (_, book) => book.Name.ToUpper())
                    .Generate()).ToList();
        }





        private static async Task<List<Book>> GetPreconfiguredBooksAsync(List<Author> authors, List<Genre> genres)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var content = memoryStream.ToArray();
            string[] bookNames =
            {
            ".NET Bot Black Sweatshirt",
            ".NET Black & White Mug",
            "Prism White T-Shirt",
            ".NET Foundation Sweatshirt",
            "Roslyn Red Sheet",
            ".NET Blue Sweatshirt",
            "Roslyn Red T-Shirt",
            "Kudu Purple Sweatshirt"
        };



            return bookNames.ToList().Select(bookName =>
                new Faker<Book>()
                    .RuleFor(book => book.Title, bookName)
                    .RuleFor(book => book.Description, (_, book) => book.Title.ToUpper())
                    .RuleFor(book => book.ReleaseDate, DateTime.Now)
                    .RuleFor(book => book.Content, content)
                    .RuleFor(book => book.Status, (Status)0)
                    .RuleFor(book => book.ImageUrl, "https://play-lh.googleusercontent.com/_tslXR7zUXgzpiZI9t70ywHqWAxwMi8LLSfx8Ab4Mq4NUTHMjFNxVMwTM1G0Q-XNU80")
                    .RuleFor(book => book.ViewCount, 0)
                    .Generate()).ToList();
            
        }

        
    }
}

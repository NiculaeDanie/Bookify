

using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Bookify.Domain.Model
{

    public class Book
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<AuthorBook> AuthorBook { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public ICollection<BookGenre> BookGenre { get; set; }
        public ICollection<UserBook> UserBook { get; set; }
        public ICollection<UserFavorites> UserFavorites { get; set; }
        public int ViewCount { get; set; }

        public Book()
        {

        }
        public Book(string title)
        {
            this.Title = title;
        }
        public Book(string title, DateTime releaseDate, string descriprion)
        {
            this.Title = title;
            this.ReleaseDate = releaseDate;
            this.Description = descriprion;
            this.Status = (Status)0;
        }


        public void PublishBook()
        {
            this.Status = (Status)1;
        }



        public void Increment()
        {
            ViewCount += 1;
        }
    }

    public enum Status
    {
        Released,
        ComingSoon
    }
}
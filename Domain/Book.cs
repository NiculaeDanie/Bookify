

using Domain.Design_Patterns;

namespace Bookify.Domain.Model
{

    public class Book: IBook
    {
        public int id { get; set; }
        public string title { get; set; }
        private List<Author> author { get; set; }
        public DateTime releaseDate { get; set; }
        public string descriprion { get; set; }
        public Status status { get; set; }
        public List<string> genre { get; set; } 
        public string content { get; set; }

        public Book()
        {

        }
        public Book(string title, List<Author> author, DateTime releaseDate, string descriprion, List<string> genre,string content)
        {
            this.title = title;
            this.author = author;
            this.releaseDate = releaseDate;
            this.descriprion = descriprion;
            this.status = (Status)1;
            this.genre = genre;
            this.content = content;
        }
        public void PublishBook()
        {
            this.status = (Status)1;
        }

        public void DisplayContents()
        {
            Console.WriteLine(this.content);
        }
    }

    public enum Status
    {
        Released,
        ComingSoon
    }
}
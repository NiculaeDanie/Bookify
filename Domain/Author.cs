
namespace Bookify.Domain.Model
{
    public class Author
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Book> books { get; set; }
        public string description { get; set; }

        public Author(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}

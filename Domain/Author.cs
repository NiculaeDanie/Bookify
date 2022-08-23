
using Domain;

namespace Bookify.Domain.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AuthorBook> AuthorBook { get; set; }
        public string Description { get; set; }
        public Author()
        {

        }
        public Author(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}

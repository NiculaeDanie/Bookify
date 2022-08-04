

namespace Bookify.Domain.Model
{
    public class User
    {
        public int id { get; set; }
        private string email { get; set; }
        private string password { get; set; }
        public string name { get; set; }
        public List<Book> history { get; set; }
        public User(int id, string email, string password, string name, List<Book> history)
        {
            this.id = id;
            this.email = email;
            this.password = password;
            this.name = name;
            this.history = history;
        }
        public User(string email, string password, string name)
        {
            this.email = email;
            this.password = password;
            this.name = name;
            this.history = new List<Book>();
        }
    }
}

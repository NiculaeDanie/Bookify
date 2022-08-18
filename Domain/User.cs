

namespace Bookify.Domain.Model
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
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
        public List<string> GetUserPreferences()
        {
            List<string> genre = new List<string>();
            foreach (var o in history)
            {
                foreach (var i in o.genre)
                {
                    genre.Add(i);
                }
            }
            var resunt = from c in genre
                         group c by c into p
                         orderby p.Count() descending
                         select p.Key;
            return resunt.ToList();
        }
    }
}

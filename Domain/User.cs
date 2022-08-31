

using Domain;

namespace Bookify.Domain.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public ICollection<UserBook> UserBook { get; set; }
        public ICollection<UserFavorites> UserFavorites { get; set; }

        public User()
        {

        }
        public User(string email, string password, string name)
        {
            this.Email = email;
            this.Password = password;
            this.Name = name;
        }
        public List<Genre> GetUserPreferences()
        {
            List<Genre> genre = new List<Genre>();
            foreach (var o in UserBook)
            {
                foreach (var i in o.Book.BookGenre)
                {
                    genre.Add(i.Genre);
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

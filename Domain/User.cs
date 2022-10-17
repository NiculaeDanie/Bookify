

using Domain;
using Microsoft.AspNetCore.Identity;

namespace Bookify.Domain.Model
{
    public class User: IdentityUser
    {
        public ICollection<UserBook> UserBook { get; set; }
        public ICollection<UserFavorites> UserFavorites { get; set; }

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

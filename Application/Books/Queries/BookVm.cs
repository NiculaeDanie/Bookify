using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries
{
    public class BookVm
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<Author> author { get; set; }
        public DateTime releaseDate { get; set; }
        public string description { get; set; }
        public Status status { get; set; }
        public List<string> genre { get; set; }

    }
}

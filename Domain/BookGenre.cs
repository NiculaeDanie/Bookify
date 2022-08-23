﻿using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BookGenre
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public Book Book { get; set; }
        public Genre Genre { get; set; }
    }
}

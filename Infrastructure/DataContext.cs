using Bookify.Domain.Model;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DataContext: IdentityDbContext<User>
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseSqlServer(@"Data Source=DESKTOP-MHJDP0S\SQLEXPRESS;Initial Catalog=Bookify;Trusted_Connection=True;");


    }
}

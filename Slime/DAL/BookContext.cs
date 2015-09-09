using Slime.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Slime.DAL
{
    public class BookContext : DbContext
    {
        public BookContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

    }
}
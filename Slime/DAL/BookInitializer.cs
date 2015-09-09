using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Slime.Models;

namespace Slime.DAL
{
    public class BookInitializer: DropCreateDatabaseIfModelChanges<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            var author = new Author
            {
                Name = "Tester",
                Publisher = "CSDN"
            };

            var author2 = new Author
            {
                Name = "Bluffing",
                Publisher = "Stackoverflow"
            };

            var books = new List<Book>
            {
                new Book
                {
                    Author = author,
                    Title = "TextBook"
                },
                new Book
                {
                    Author = author,
                    Title= "PicBook"
                },
                new Book
                {
                    Author = author2,
                    Title = "NewBook"
                }
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();
        }
    }
}
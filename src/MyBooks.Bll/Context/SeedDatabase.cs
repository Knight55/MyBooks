using System;
using System.Linq;
using MyBooks.Bll.Entities;

namespace MyBooks.Bll.Context
{
    public static class SeedDatabase
    {
        public static void Seed(this ApplicationDbContext context)
        {
            if (!context.Books.Any())
            {
                var book1 = new Book
                {
                    Title = "The Way of Kings",
                    Genre = "Fantasy",
                    GoodreadsId = "7235533-the-way-of-kings",
                    CoverImagePath = "thewayofkings.jpg"
                };
                var book2 = new Book
                {
                    Title = "The Black Prism",
                    Genre = "Fantasy",
                    GoodreadsId = "7165300-the-black-prism",
                    CoverImagePath = "blackprism.jpg"
                };
                var book3 = new Book
                {
                    Title = "The Eye of the World",
                    Genre = "Fantasy",
                    GoodreadsId = "228665.The_Eye_of_the_World",
                    CoverImagePath = "theeyeoftheworld.jpg"
                };
                var book4 = new Book
                {
                    Title = "Red Rising",
                    Genre = "Scifi",
                    GoodreadsId = "15839976-red-rising",
                    CoverImagePath = "redrising.jpg"
                };
                var book5 = new Book
                {
                    Title = "The Broken Eye",
                    Genre = "Fantasy",
                    GoodreadsId = "12652457-the-broken-eye",
                    CoverImagePath = "brokeneye.jpg"
                };

                var author1 = new Author {Name = "Brandon Sanderson", DateOfBirth = new DateTime(1975, 12, 19)};
                var author2 = new Author {Name = "Brent Weeks", DateOfBirth = new DateTime(1977, 3, 7)};
                var author3 = new Author {Name = "Robert Jordan", DateOfBirth = new DateTime(1948, 10, 17)};
                var author4 = new Author {Name = "Pierce Brown", DateOfBirth = new DateTime(1988, 1, 28)};

                var bookAuthor1 = new BookAuthor {Author = author1, Book = book1};
                var bookAuthor2 = new BookAuthor {Author = author2, Book = book2};
                var bookAuthor3 = new BookAuthor {Author = author3, Book = book3};
                var bookAuthor4 = new BookAuthor {Author = author4, Book = book4};
                var bookAuthor5 = new BookAuthor {Author = author2, Book = book5};

                book1.BookAuthors.Add(bookAuthor1);
                book2.BookAuthors.Add(bookAuthor2);
                book3.BookAuthors.Add(bookAuthor3);
                book4.BookAuthors.Add(bookAuthor4);
                book5.BookAuthors.Add(bookAuthor5);

                var publisher1 = new Publisher {Name = "Tor Books"};
                var publisher2 = new Publisher {Name = "Tom Doherty"};

                var edition1 = new Edition
                {
                    Book = book1,
                    Publisher = publisher1,
                    Format = Format.Hardcover,
                    IsbnNumber = "0765326353",
                    NumberOfPages = 1007,
                    DateOfPublish = new DateTime(2010, 8, 31)
                };
                var edition2 = new Edition
                {
                    Book = book1,
                    Publisher = publisher2,
                    Format = Format.Paperback,
                    IsbnNumber = "0765365278",
                    NumberOfPages = 1258,
                    DateOfPublish = new DateTime(2011, 5, 24)
                };

                context.Books.AddRange(book1, book2, book3, book4, book5);
                context.Authors.AddRange(author1, author2, author3, author4);
                context.Publishers.AddRange(publisher1, publisher2);
                context.Editions.AddRange(edition1, edition2);

                context.SaveChanges();
            }
        }
    }
}
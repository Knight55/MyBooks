using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyBooks.Bll.Entities;

namespace MyBooks.Bll.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(c => c.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>()
                .HasData(
                    new Book
                    {
                        Id = 1, Title = "The Way of Kings", Genre = "Fantasy",
                        GoodreadsId = "7235533-the-way-of-kings", CoverImagePath = "thewayofkings.jpg"
                    },
                    new Book
                    {
                        Id = 2, Title = "The Black Prism", Genre = "Fantasy",
                        GoodreadsId = "7165300-the-black-prism", CoverImagePath = "blackprism.jpg"
                    },
                    new Book
                    {
                        Id = 3, Title = "The Eye of the World", Genre = "Fantasy",
                        GoodreadsId = "228665.The_Eye_of_the_World", CoverImagePath = "theeyeoftheworld.jpg"
                    },
                    new Book
                    {
                        Id = 4, Title = "Red Rising", Genre = "Scifi",
                        GoodreadsId = "7235533-the-way-of-kings", CoverImagePath = "wayofkings.jpg"
                    },
                    new Book
                    {
                        Id = 5, Title = "The Broken Eye", Genre = "Fantasy",
                        GoodreadsId = "12652457-the-broken-eye", CoverImagePath = "brokeneye.jpg"
                    });

            builder.Entity<Author>()
                .HasData(
                    new Author { Id = 1, Name = "Brandon Sanderson", DateOfBirth = new DateTime(1975, 12, 19) },
                    new Author { Id = 2, Name = "Brent Weeks", DateOfBirth = new DateTime(1977, 3, 7) },
                    new Author { Id = 3, Name = "Robert Jordan", DateOfBirth = new DateTime(1948, 10, 17) },
                    new Author { Id = 4, Name = "Pierce Brown", DateOfBirth = new DateTime(1988, 1, 28)} );

            builder.Entity<BookAuthor>()
                .HasData(
                    new BookAuthor { Id = 1, BookId = 1, AuthorId = 1 },
                    new BookAuthor { Id = 2, BookId = 2, AuthorId = 2 },
                    new BookAuthor { Id = 3, BookId = 3, AuthorId = 3 },
                    new BookAuthor { Id = 4, BookId = 4, AuthorId = 4 },
                    new BookAuthor { Id = 5, BookId = 5, AuthorId = 2 });

            builder.Entity<Publisher>()
                .HasData(
                    new Publisher {Id = 1, Name = "Tor Books"},
                    new Publisher {Id = 2, Name = "Tom Doherty"});

            builder.Entity<Edition>()
                .Property(e => e.Format)
                .HasConversion<string>()
                .HasDefaultValue(Format.Paperback);

            builder.Entity<Edition>()
                .HasData(
                    new Edition
                    {
                        Id = 1, BookId = 1, PublisherId = 1, Format = Format.Hardcover, IsbnNumber = "0765326353",
                        NumberOfPages = 1007, DateOfPublish = new DateTime(2010, 8, 31)
                    },
                    new Edition
                    {
                        Id = 2, BookId = 1, PublisherId = 2, Format = Format.Paperback, IsbnNumber = "0765365278",
                        NumberOfPages = 1258, DateOfPublish = new DateTime(2011, 5, 24)
                    });
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ReadingStatus> ReadingStatuses { get; set; }
    }
}
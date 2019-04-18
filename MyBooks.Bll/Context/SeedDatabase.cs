using Microsoft.EntityFrameworkCore.Internal;
using MyBooks.Bll.Entities;

namespace MyBooks.Bll.Context
{
    public static class SeedDatabase
    {
        public static void Seed(this ApplicationDbContext context)
        {
            if (!context.Books.Any())
            {
                var book_wayofkings = new Book
                {
                    Title = "The Way Of Kings",
                    CoverImagePath = "wayofkings.jpg",
                    Genre = "Fantasy"
                };
                var book_redrising = new Book
                {
                    Title = "Red Rising",
                    CoverImagePath = "redrising.jpg",
                    Genre = "Science Fiction"
                };
                var book_theeyeoftheworld = new Book
                {
                    Title = "The Eye of the World",
                    CoverImagePath = "theeyeoftheworld.jpg",
                    Genre = "Fantasy"
                };

                context.Books.Add(book_wayofkings);
                context.Books.Add(book_redrising);
                context.Books.Add(book_theeyeoftheworld);

                context.SaveChanges();
            }
        }
    }
}
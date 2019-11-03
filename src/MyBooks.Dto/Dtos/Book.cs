using System.Collections.Generic;
using MyBooks.Dto.Goodreads;

namespace MyBooks.Dto.Dtos
{
    public class Book
    {
        public Book() {}

        public Book(GoodreadsWork work)
        {
            GoodreadsId = work.Book.Id;
            Title = work.Book.Title;
            CoverImageUrl = work.Book.ImageUrl;
        }

        public Book(GoodreadsBook book)
        {
            Isbn = book.Isbn;
            GoodreadsId = book.Id;
            Title = book.Title;
            CoverImageUrl = book.ImageUrl;
            Summary = book.Description;
            //foreach (var bookAuthor in book.Authors)
            //{
            //    AuthorIds.Add(int.Parse(bookAuthor.Id));
            //}
        }
        
        public int Id { get; set; }

        public string Isbn { get; set; }

        public string GoodreadsId { get; set; }

        /// <summary>
        /// The title of the book.
        /// </summary>
        public string Title { get; set; }

        public string CoverImageUrl { get; set; }

        public string Summary { get; set; }

        public string Genre { get; set; }

        public double Rating { get; set; }

        public string GoodreadsUrl { get; set; }

        public List<int> AuthorIds { get; set; } = new List<int>();
    }
}
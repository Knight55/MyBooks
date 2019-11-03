using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MyBooks.Dto.Dtos
{
    public class Book
    {
        public Book()
        {
        }

        public Book(Work work)
        {
            Title = work.Book.Title;
            CoverUrl = work.Book.ImageUrl;
            Rating = double.Parse(work.AverageRating, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
        }

        public int Id { get; set; }

        /// <summary>
        /// The title of the book.
        /// </summary>
        [Required(ErrorMessage = "Book title is required", AllowEmptyStrings = false)]
        public string Title { get; set; }

        public string CoverUrl { get; set; }

        public string Summary { get; set; }

        public string Genre { get; set; }

        public double Rating { get; set; }

        public string GoodreadsUrl { get; set; }

        public List<int> AuthorIds { get; set; }
    }
}
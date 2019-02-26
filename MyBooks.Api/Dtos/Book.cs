using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyBooks.Bll.Entities;

namespace MyBooks.Api.Dtos
{
    public class Book
    {
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

        public List<Author> Authors { get; set; }
    }
}
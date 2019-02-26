using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBooks.Bll.Entities
{
    [Table("BOOK")]
    public class Book
    {
        public int Id { get; set; }

        public string GoodreadsId { get; set; }

        public string Title { get; set; }

        public string CoverImagePath { get; set; }

        public string Summary { get; set; }

        public string Genre { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }

        public ICollection<Edition> Editions { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<BookOwnership> BookOwnerships { get; set; }
    }
}
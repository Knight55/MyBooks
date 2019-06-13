using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBooks.Bll.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string GoodreadsId { get; set; }

        public string Title { get; set; }

        public string CoverImagePath { get; set; }

        public string Summary { get; set; }

        public string Genre { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

        public ICollection<Edition> Editions { get; set; } = new List<Edition>();

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        public ICollection<BookOwnership> BookOwnerships { get; set; } = new List<BookOwnership>();
    }
}
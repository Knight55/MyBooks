using System;
using System.Collections.Generic;

namespace MyBooks.Dal.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
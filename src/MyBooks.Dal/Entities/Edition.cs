using System;

namespace MyBooks.Dal.Entities
{
    public class Edition
    {
        public int Id { get; set; }

        public string IsbnNumber { get; set; }

        public DateTime DateOfPublish { get; set; }

        public int NumberOfPages { get; set; }

        public Format Format { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
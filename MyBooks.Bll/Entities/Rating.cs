﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MyBooks.Bll.Entities
{
    [Table("RATING")]
    public class Rating
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public string Text { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
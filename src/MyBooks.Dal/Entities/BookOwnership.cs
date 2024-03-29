﻿using System;

namespace MyBooks.Dal.Entities
{
    public class BookOwnership
    {
        public int Id { get; set; }

        public DateTime DateOfAcquisition { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
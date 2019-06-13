using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBooks.Dto.Dtos
{
    public class Author
    {
        /// <summary>
        /// The identifier of the author.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the author.
        /// </summary>
        [Required(ErrorMessage = "Author name is required", AllowEmptyStrings = false)]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<int> BookIds { get; set; }
    }
}
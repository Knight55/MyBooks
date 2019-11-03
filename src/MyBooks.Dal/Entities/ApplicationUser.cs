using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyBooks.Dal.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {
        public ICollection<BookOwnership> BookOwnerships { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<ReadingStatus> ReadingStatuses { get; set; }
    }
}
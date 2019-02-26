using System.ComponentModel.DataAnnotations.Schema;

namespace MyBooks.Bll.Entities
{
    [Table("READINGSTATUS")]
    public class ReadingStatus
    {
        public int Id { get; set; }

        public int EditionId { get; set; }
        public Edition Edition { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int NumberOfPagesRead { get; set; }
    }
}
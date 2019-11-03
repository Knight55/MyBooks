namespace MyBooks.Dal.Entities
{
    public class ReadingStatus
    {
        public int Id { get; set; }

        public int NumberOfPagesRead { get; set; }

        public int EditionId { get; set; }
        public Edition Edition { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
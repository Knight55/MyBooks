using System.Collections.Generic;

namespace MyBooks.Dal.Entities
{
    public class Publisher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Edition> Editions { get; set; } = new List<Edition>();
    }
}
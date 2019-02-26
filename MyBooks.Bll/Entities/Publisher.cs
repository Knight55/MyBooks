using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBooks.Bll.Entities
{
    [Table("PUBLISHER")]
    public class Publisher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Edition> Editions { get; set; }
    }
}
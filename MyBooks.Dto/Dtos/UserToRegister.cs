using System.ComponentModel.DataAnnotations;

namespace MyBooks.Dto.Dtos
{
    public class UserToRegister
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Level { get; set; }
    }
}
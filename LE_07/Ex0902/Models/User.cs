using System.ComponentModel.DataAnnotations;

namespace Ex0902.Models
{
    public class User
    {
        public int? UserId { get; private set; }

        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required]
        [StringLength(50)]
        public required string Password { get; set; }
    }
}

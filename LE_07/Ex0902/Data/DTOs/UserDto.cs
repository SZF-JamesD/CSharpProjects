using System.ComponentModel.DataAnnotations;

namespace Ex0902.Data.DTOs
{
    public class UserDto
    {
        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required]
        [StringLength(50)]
        public required string Password { get; set; }
    }
}

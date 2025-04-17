using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Ex0902.Models
{
    public class Customer
    {
        public int? CustomerId {  get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public required string Street { get; set; }

        [Required]
        [StringLength(10)]
        public required string HouseNo { get; set; }

        [Required]
        public required int PostCode { get; set; }

        [Required]
        [StringLength(50)]
        public required string City { get; set; }

        [Required]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        public required int CreatedBy { get; set; }
    }
}

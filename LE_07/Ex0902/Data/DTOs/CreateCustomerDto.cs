using System.ComponentModel.DataAnnotations;

namespace Ex0902.Data.DTOs
{
    public class CreateCustomerDto
    {
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

        public Dictionary<string, object> ToDict(int createdBy)
        {
            return new Dictionary<string, object>
            {
                { "first_name", FirstName },
                { "last_name", LastName },
                { "street", Street },
                { "house_no", HouseNo },
                { "post_code", PostCode },
                { "city", City },
                { "email", Email },
                { "created_by", createdBy }
            };
        }
    }
}

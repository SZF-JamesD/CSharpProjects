using System.ComponentModel.DataAnnotations;

namespace Ex0902.Data.DTOs
{
    public class UpdateCustomerDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string HouseNo { get; set; } = string.Empty;

        [Required]
        public int PostCode { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        public Dictionary<string, object> ToDict()
        {
            return new Dictionary<string, object>
            {
                { "first_name", FirstName },
                { "last_name", LastName },
                { "street", Street },
                { "house_no", HouseNo },
                { "post_code", PostCode },
                { "city", City },
                { "email", Email }
            };
        }
    }
}


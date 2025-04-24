using System.ComponentModel.DataAnnotations;

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

        public static Customer FromDict(Dictionary<string, object> dict)
        {
            return new Customer
            {
                CustomerId = dict["customer_id"] as int?,
                FirstName = dict["first_name"]?.ToString() ?? string.Empty,
                LastName = dict["last_name"]?.ToString() ?? string.Empty,
                Street = dict["street"]?.ToString() ?? string.Empty,
                HouseNo = dict["house_no"]?.ToString() ?? string.Empty,
                PostCode = dict["post_code"] is int p ? p : Convert.ToInt32(dict["post_code"]!),
                City = dict["city"]?.ToString() ?? string.Empty,
                Email = dict["email"]?.ToString() ?? string.Empty,
                CreatedBy = dict["created_by"] is int c ? c : Convert.ToInt32(dict["created_by"]!)
            };
        }

        public Dictionary<string, object> ToDict(bool includeId = false)
        {
            var dict = new Dictionary<string, object>
            {
                { "first_name", FirstName },
                { "last_name", LastName },
                { "street", Street },
                { "house_no", HouseNo },
                { "post_code", PostCode },
                { "city", City },
                { "email", Email },
                { "created_by", CreatedBy }
            };

            if (includeId && CustomerId.HasValue)
                dict["customer_id"] = CustomerId.Value;

            return dict;
        }
    }
}

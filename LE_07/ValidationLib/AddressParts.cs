namespace ValidationLib
{
    public class AddressParts
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Apartment { get; set; } //optional
        public string PostalCode { get; set; }
        public string City { get; set; }


        public override string ToString()
        {
            return $"{Street} {Number}" + (string.IsNullOrWhiteSpace(Apartment) ? "" : $"/{Apartment}") + $" {PostalCode} {City}";
        }   
    }   
}

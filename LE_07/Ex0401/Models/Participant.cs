namespace Ex0401.Models
{
    public class Participant
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public Participant(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }

        public string DisplayInfo()
        {
            return $"Participant Name: {FullName}, Email Address: {Email}";
        }
    }
}

namespace Ex0301.Models
{
    public abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        protected Person(string firstName, string lastName) 
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public abstract string DisplayInfo();
    }
}

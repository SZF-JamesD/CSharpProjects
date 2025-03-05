namespace Ex0203.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Phonenumber { get; set; }


        public Person(string firstName, string lastName, int age, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Phonenumber = phoneNumber;

        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}\nAge: {Age}\nPhone Number: {Phonenumber}\n";
        }
    }
}
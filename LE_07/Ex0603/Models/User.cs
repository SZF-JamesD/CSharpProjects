namespace Ex0603.Models
{
    internal class User
    {
        public string Username { get; set; }
        private string _password;

        public User(string username, string password) 
        {
            Username = username;
            _password = password;
        }

        public bool ValidatePassword(string password)
        {
            return _password == password;
        }
    }
}

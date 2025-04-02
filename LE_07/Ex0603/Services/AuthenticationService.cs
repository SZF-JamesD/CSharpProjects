namespace Ex0603.Services
{
    public class AuthenticationService
    {
        private readonly string _validUsername = "admin12321";
        private readonly string _validPassword = "password65456";

        public bool ValidateUser(string username, string password)
        {
            return username == _validUsername && password == _validPassword;
        }
    }
}

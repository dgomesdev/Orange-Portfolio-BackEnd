namespace Orange_Portfolio_BackEnd.Application.Services
{
    public class CryptographyService
    {
        // Function to encrypt the password
        public static string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Function to verify a hashed password
        public static bool Verify(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}

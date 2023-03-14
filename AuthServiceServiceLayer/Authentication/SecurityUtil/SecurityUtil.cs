using System.Web.Helpers;

namespace AuthServiceServiceLayer.Authentication.SecurityUtil
{
    public class SecurityUtil : ISecurityUtil
    {
        public string CreateSalt()
        {
            return Crypto.GenerateSalt();
        }

        public string HashPassword(string password, string salt)
        {
            return Crypto.HashPassword(password + salt);
        }

        public bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, password + salt);
        }
    }
}

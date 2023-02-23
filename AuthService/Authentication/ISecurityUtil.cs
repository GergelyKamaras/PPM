namespace AuthService.Authentication;

public interface ISecurityUtil
{
    string CreateSalt();
    string HashPassword(string password, string salt);
    bool VerifyPassword(string password, string salt, string hashedPassword);
}
using AuthServiceModelLibrary.Enums;

namespace AuthServiceModelLibrary.ApplicationUser;

public interface IApplicationUser
{
    string Email { get; set; }
    string UserName { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Salt { get; set; }
    string PasswordHash { get; set; }
    UserRole Role { get; set; }
}
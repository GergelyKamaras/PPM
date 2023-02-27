using AuthServiceModelLibrary.ApplicationUser;

namespace AuthService.Authentication
{
    public interface IAuthOperations
    {
        bool Register(ApplicationUser user);
    }
}

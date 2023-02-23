using PPMModelLibrary.Models.Users;

namespace AuthService.Authentication
{
    public interface IAuthOperations
    {
        bool Register(ApplicationUser user);
    }
}

using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;

namespace AuthService.Authentication
{
    public interface IAuthOperations
    {
        bool Register(ApplicationUser user);
        ApplicationUser Login(IUserLoginDTO loginDTO);
    }
}

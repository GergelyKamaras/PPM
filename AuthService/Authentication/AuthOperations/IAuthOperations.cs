using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;

namespace AuthService.Authentication.AuthOperations
{
    public interface IAuthOperations
    {
        bool Register(ApplicationUser user);
        ApplicationUser VerifyLoginDTO(IUserLoginDTO loginDTO);
    }
}

using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;

namespace AuthService.ModelConverter;

public interface IApplicationUserFactory
{
    ApplicationUser Converter(UserRegistrationDTO userDTO);
}
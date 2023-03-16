using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;

namespace AuthServiceServiceLayer.ModelConverter;

public interface IApplicationUserFactory
{
    ApplicationUser Converter(UserRegistrationDTO userDTO);
}
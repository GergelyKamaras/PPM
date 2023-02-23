using PPMModelLibrary.Models.InputDTOs;
using PPMModelLibrary.Models.Users;

namespace AuthService.ModelConverter;

public interface IApplicationUserFactory
{
    ApplicationUser Converter(UserRegistrationDTO userDTO);
}
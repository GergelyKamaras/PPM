using AuthService.Authentication.SecurityUtil;
using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;

namespace AuthService.ModelConverter
{
    public class ApplicationUserFactory : IApplicationUserFactory
    {
        private readonly ISecurityUtil _secUtil;
        public ApplicationUserFactory(ISecurityUtil secUtil)
        {
            _secUtil = secUtil;
        }
        public ApplicationUser Converter(UserRegistrationDTO userDTO)
        {
            string salt = _secUtil.CreateSalt();

            Guid id = Guid.NewGuid();

            return new ApplicationUser()
            {
                Id = id.ToString(),
                Email = userDTO.Email,
                UserName = userDTO.Username,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Salt = salt,
                PasswordHash = _secUtil.HashPassword(userDTO.Password, salt),
                Role = userDTO.Role
            };
        }
    }
}

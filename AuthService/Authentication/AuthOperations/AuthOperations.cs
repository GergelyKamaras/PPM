using AuthService.DataAccess.UserTableQueries;
using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;
using AuthService.Authentication.SecurityUtil;


namespace AuthService.Authentication.AuthOperations
{
    public class AuthOperations : IAuthOperations
    {
        private readonly IUserTableQueries _queries;
        private readonly ISecurityUtil _securityUtil;
        public AuthOperations(IUserTableQueries queries, ISecurityUtil securityUtil)
        {
            _queries = queries;
            _securityUtil = securityUtil;
        }
        public bool Register(ApplicationUser user)
        {
            if (_queries.GetUserByEmail(user.Email) != null)
            {
                throw new ArgumentException("Email address already registered");
            }
            if (_queries.GetUserByUsername(user.UserName) != null)
            {
                throw new ArgumentException("Username already taken");
            }

            _queries.AddUser(user);

            return true;
        }

        public ApplicationUser VerifyLoginDTO(IUserLoginDTO loginDTO)
        {
            ApplicationUser user = _queries.GetUserByEmail(loginDTO.Email);
            if (user == null)
            {
                throw new ArgumentException("No user with given email exists in database");
            }

            if (!_securityUtil.VerifyPassword(loginDTO.Password, user.Salt, user.PasswordHash))
            {
                throw new ArgumentException("Password verification error");
            }

            return user;
        }
    }
}

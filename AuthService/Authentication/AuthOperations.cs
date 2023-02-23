using AuthService.DataAccess;
using AuthService.DataAccess.UserTableQueries;
using PPMModelLibrary.Models.Users;


namespace AuthService.Authentication
{
    public class AuthOperations : IAuthOperations
    {
        private readonly IUserTableQueries _queries;
        public AuthOperations(IUserTableQueries queries)
        {
            _queries = queries;
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
    }
}

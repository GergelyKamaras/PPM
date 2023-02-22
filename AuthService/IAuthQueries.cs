using DataAccess.Models.Users;

namespace AuthService
{
    public interface IAuthQueries
    {
        bool Register(ApplicationUser user);
    }
}

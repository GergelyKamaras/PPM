using DataAccess;
using DataAccess.Models.Users;

namespace AuthService
{
    public class AuthQueries : IAuthQueries
    {
        private AppDbContext _db;
        public AuthQueries(AppDbContext dbContext)
        {
            _db = dbContext;
        }
        public bool Register(ApplicationUser user)
        {
            if (_db.Users.Any(u => u.Email == user.Email))
            {
                throw new ArgumentException("Email address already registered");
            }
            if (_db.Users.Any(u => u.UserName == user.UserName))
            {
                throw new ArgumentException("Username already taken");
            }
            else
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return true;
            }
        }
    }
}

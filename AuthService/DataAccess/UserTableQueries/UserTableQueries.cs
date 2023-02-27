using AuthServiceModelLibrary.ApplicationUser;

namespace AuthService.DataAccess.UserTableQueries
{
    public class UserTableQueries : IUserTableQueries
    {
        private readonly AuthDbContext _db;

        public UserTableQueries(AuthDbContext db)
        {
            _db = db;
        }

        public void AddUser(ApplicationUser user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
        public void UpdateUser(ApplicationUser user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            _db.Users.Remove(_db.Users.First(u => u.Id == id));
            _db.SaveChanges();
        }

        public ApplicationUser? GetUserById(string id)
        {
            return _db.Users.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser? GetUserByEmail(string email)
        {
            return _db.Users.FirstOrDefault(u => u.Email == email);
        }

        public ApplicationUser? GetUserByUsername(string username)
        {
            return _db.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}

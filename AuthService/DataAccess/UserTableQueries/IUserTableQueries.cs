using AuthServiceModelLibrary.ApplicationUser;

namespace AuthService.DataAccess.UserTableQueries;

public interface IUserTableQueries
{
    public void AddUser(IApplicationUser user);
    public void UpdateUser(IApplicationUser user);
    public void DeleteUser(string id);
    public ApplicationUser? GetUserById(string id);
    public ApplicationUser? GetUserByEmail(string email);
    public ApplicationUser? GetUserByUsername(string username);
}
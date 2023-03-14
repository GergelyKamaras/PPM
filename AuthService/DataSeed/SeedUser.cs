using AuthServiceAPI.Controller;
using AuthServiceDataAccess.UserTableQueries;
using AuthServiceModelLibrary.DTOs;

namespace AuthServiceAPI.DataSeed
{
    public static class SeedUser
    {
        public static async void Init(AuthController controller, IUserTableQueries queries, string password)
        {
            UserRegistrationDTO user = new UserRegistrationDTO()
            {
                Email = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Von Admin",
                Password = password,
                Role = "Administrator",
                Username = "admin@admin.com"
            };

            if (queries.GetUserByEmail(user.Email) == null && queries.GetUserByUsername(user.Username) == null)
            {
                try
                {
                    await controller.Register(user);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

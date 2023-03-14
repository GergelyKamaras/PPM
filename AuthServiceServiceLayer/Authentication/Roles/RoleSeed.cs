using Microsoft.AspNetCore.Identity;

namespace AuthServiceServiceLayer.Authentication.Roles
{
    public static class RoleSeed
    {
        public static async Task InitRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (string role in Enum.GetNames(typeof(UserRoles)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}

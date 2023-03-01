using Microsoft.AspNetCore.Identity;

namespace AuthService.Authentication.Roles.Validator
{
    public class RoleValidator : IRoleValidator
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleValidator(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> validate(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}

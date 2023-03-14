using Microsoft.AspNetCore.Identity;

namespace AuthServiceServiceLayer.Authentication.Roles.Validator
{
    public class RoleValidator : IRoleValidator
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleValidator(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> Validate(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}

namespace AuthService.Authentication.Roles.Validator;

public interface IRoleValidator
{
    Task<bool> Validate(string roleName);
}
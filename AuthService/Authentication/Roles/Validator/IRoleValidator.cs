namespace AuthService.Authentication.Roles.Validator;

public interface IRoleValidator
{
    Task<bool> validate(string roleName);
}
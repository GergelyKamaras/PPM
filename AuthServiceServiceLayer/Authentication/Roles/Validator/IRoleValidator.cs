namespace AuthServiceServiceLayer.Authentication.Roles.Validator;

public interface IRoleValidator
{
    Task<bool> Validate(string roleName);
}
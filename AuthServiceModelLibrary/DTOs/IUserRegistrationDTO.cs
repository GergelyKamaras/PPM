namespace AuthServiceModelLibrary.DTOs;

public interface IUserRegistrationDTO
{
    string Email { get; set; }
    string Username { get; set; }
    string Password { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Role { get; set; }
}
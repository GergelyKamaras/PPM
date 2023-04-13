namespace AuthServiceModelLibrary.DTOs;

public interface ILoginResultDTO
{
    string Message { get; set; }
    string Token { get; set; }
}
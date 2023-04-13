namespace AuthServiceModelLibrary.DTOs
{
    public class LoginResultDTO : ILoginResultDTO
    {
        public string Message { get; set; }
        public string Token { get; set; }
    }
}

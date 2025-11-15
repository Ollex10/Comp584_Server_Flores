namespace Comp584_Server_Flores.DTOs
{
    public class LoginResponse
    {
        public required bool success { get; set; }
        public required string Message { get; set; }
        public required string token { get; set; }

    }
}

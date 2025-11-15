namespace comp584Server.DTOs
{
    public class LoginResponse
    {
        public bool Sucess { get; set; }
        public required string Message { get; set; }
        public required string Token { get; set; }
    }
}

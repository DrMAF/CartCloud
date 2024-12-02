namespace Core.DTOs
{
    public class LoginResultVM
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime? Expiration { get; set; }
    }
}

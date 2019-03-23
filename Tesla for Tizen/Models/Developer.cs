namespace TeslaTizen.Models
{
    public class Developer
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public long ExpiresIn { get; set; }
        public long CreatedAt { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

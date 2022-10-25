using System;

namespace Met.DTOs
{
    public class AuthResultResponse
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}

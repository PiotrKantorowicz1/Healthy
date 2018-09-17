namespace Healthy.Infrastructure.Settings
{
    public class JwtTokenSettings
    {
        public string SecretKey { get; set; }
        public int ExpiryDays { get; set; }
        public string Issuer { get; set; }
        public bool ValidateIssuer { get; set; }
    }
}
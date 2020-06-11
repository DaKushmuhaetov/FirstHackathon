namespace FirstHackathon.Models.Authentication
{
    public sealed class JwtAuthHouseOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public int LifeTimeMinutes { get; set; }
    }
}

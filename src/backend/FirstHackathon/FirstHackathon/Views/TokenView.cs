using System.Text.Json.Serialization;

namespace FirstHackathon.Views
{
    public sealed class TokenView
    {
        /// <summary>
        /// The access token issued by the authorization server
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// The type of the token issued. Example: Bearer 
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "Bearer";
    }
}

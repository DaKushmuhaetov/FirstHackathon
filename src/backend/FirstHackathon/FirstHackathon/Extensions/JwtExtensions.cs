using System.Security.Claims;

namespace FirstHackathon.Extensions
{
    internal static class JwtExtensions
    {
        public static string GetAddress(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(o => o.Type.Equals(ClaimTypes.StreetAddress))?.Value;
        }
    }
}

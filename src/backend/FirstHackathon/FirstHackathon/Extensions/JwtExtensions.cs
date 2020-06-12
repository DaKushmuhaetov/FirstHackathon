using System;
using System.Security.Claims;

namespace FirstHackathon.Extensions
{
    internal static class JwtExtensions
    {
        public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.FindFirst(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);
        }

        public static string GetAddress(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(o => o.Type.Equals(ClaimTypes.StreetAddress))?.Value;
        }
    }
}

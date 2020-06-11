using FirstHackathon.Models;
using FirstHackathon.Models.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Extensions
{
    public sealed class JwtAccessTokenFactory : IJwtAccessTokenFactory
    {
        private readonly JwtAuthOptions _authOptions;

        public JwtAccessTokenFactory(IOptions<JwtAuthOptions> options)
        {
            _authOptions = options.Value;
        }

        public Task<AccessToken> Create(Person person, CancellationToken cancellationToken)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                _authOptions.Issuer,
                _authOptions.Audience,
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, person.Id.ToString()),
                    new Claim(ClaimTypes.Name, person.Name),
                    new Claim(ClaimTypes.Surname, person.Surname),
                    new Claim(ClaimTypes.Email, person.Login),
                }, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType).Claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_authOptions.LifeTimeMinutes),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecretKey)), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Task.FromResult(new AccessToken(token, TimeSpan.FromMinutes(_authOptions.LifeTimeMinutes)));
        }
    }
}

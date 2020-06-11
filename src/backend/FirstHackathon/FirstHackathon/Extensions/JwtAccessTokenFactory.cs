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
        private readonly JwtAuthPersonOptions _authPersonOptions;
        private readonly JwtAuthHouseOptions _authHouseOptions;

        public JwtAccessTokenFactory(IOptions<JwtAuthPersonOptions> personOptions, IOptions<JwtAuthHouseOptions> houseOptions)
        {
            _authPersonOptions = personOptions.Value;
            _authHouseOptions = houseOptions.Value;
        }

        public Task<AccessToken> Create(Person person, CancellationToken cancellationToken)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                _authPersonOptions.Issuer,
                _authPersonOptions.Audience,
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, person.Id.ToString()),
                    new Claim(ClaimTypes.Name, person.Name),
                    new Claim(ClaimTypes.Surname, person.Surname),
                    new Claim(ClaimTypes.StreetAddress, person.House.Address),
                    new Claim(ClaimTypes.Email, person.Login),
                }, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType).Claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_authPersonOptions.LifeTimeMinutes),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authPersonOptions.SecretKey)), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Task.FromResult(new AccessToken(token, TimeSpan.FromMinutes(_authPersonOptions.LifeTimeMinutes)));
        }

        public Task<AccessToken> Create(House house, CancellationToken cancellationToken)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                _authHouseOptions.Issuer,
                _authHouseOptions.Audience,
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, house.Id.ToString()),
                    new Claim(ClaimTypes.StreetAddress, house.Address.ToString()),
                    new Claim(ClaimTypes.Email, house.Login),
                }, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType).Claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_authHouseOptions.LifeTimeMinutes),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authHouseOptions.SecretKey)), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Task.FromResult(new AccessToken(token, TimeSpan.FromMinutes(_authHouseOptions.LifeTimeMinutes)));
        }
    }
}

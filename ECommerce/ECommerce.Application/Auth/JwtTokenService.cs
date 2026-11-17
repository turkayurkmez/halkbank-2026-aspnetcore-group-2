using ECommerce.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Application.Auth
{
    public class JwtTokenService(IOptions<JwtOptions> options) : ITokenService
    {

        private readonly JwtOptions jwtOptions = options.Value;
        public TimeSpan RefreshTokenLifeTime => TimeSpan.FromDays(jwtOptions.RefreshTokenDays);

        public AccessToken CreateAccessToken(User user)
        {
            var now = DateTime.Now;
            var expiresAt = now.AddMinutes(jwtOptions.AccessTokenMinutes);

            var desc = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                NotBefore = now,
                Expires = expiresAt,
                Claims = new Dictionary<string, object>
                {
                    [JwtRegisteredClaimNames.Sub] = user.Id.ToString(),
                    [JwtRegisteredClaimNames.Email] = user.Email,
                    [JwtRegisteredClaimNames.UniqueName] = user.FullName,
                    [JwtRegisteredClaimNames.Birthdate] = user.BirthDate,
                    ["role"] = user.Roles.Select(r => r.Name).ToArray()
                },
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)), SecurityAlgorithms.HmacSha256)


            };

            return new AccessToken(new JsonWebTokenHandler().CreateToken(desc), expiresAt);

            
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)).Replace('+', '-')
                                                                      .Replace('/', '_')
                                                                      .TrimEnd('=');
        }
    }
}

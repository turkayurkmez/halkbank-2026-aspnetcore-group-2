using ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Auth
{

    public record AccessToken(string Value, DateTime ExpriesAt);
    public interface ITokenService
    {
        AccessToken CreateAccessToken(User user);

        string GenerateRefreshToken();

        TimeSpan RefreshTokenLifeTime { get; }
    }
}

using ECommerce.Application.Features.Users.Commands.RegisterUser;
using ECommerce.Domain;
using ECommerce.Infrastructure.UnitOfWorkPattern;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Application.Auth
{
    public class AuthTokenIssuer(ITokenService tokenService, IUnitOfWork uow)
    {
        public static string Hash(string token) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

        public async Task<string> AddRefreshTokenAsync(User user, string? refreshPlain = null)
        {

            //üretilen hash veritabanında, refreshPlain metni ise istemcide:
            refreshPlain ??= tokenService.GenerateRefreshToken();

            var token = new RefreshToken
            {
                Token = Hash(refreshPlain),
                ExpiresAt = DateTime.Now.Add(tokenService.RefreshTokenLifeTime)
            };

            if (user.Id == 0)
            {
                token.User = user;
            }
            else
            {
                token.UserId = user.Id;
            }

            await uow.RefreshTokens.AddAsync(token);
            return refreshPlain;

        }

        public AuthResponse CreateResponse(User user, string refreshPlain)
        {
            var token = tokenService.CreateAccessToken(user);
            return new AuthResponse(token.Value, refreshPlain, token.ExpriesAt);
        }
    }
}

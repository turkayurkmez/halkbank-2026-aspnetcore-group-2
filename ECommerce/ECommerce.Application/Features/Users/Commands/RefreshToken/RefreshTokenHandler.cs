using ECommerce.Application.Auth;
using ECommerce.Application.Features.Users.Commands.RegisterUser;
using ECommerce.Infrastructure.UnitOfWorkPattern;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.RefreshToken
{
    public class RefreshTokenHandler(IUnitOfWork uow, ITokenService tokenService, AuthTokenIssuer authTokenIssuer)  : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var hash = AuthTokenIssuer.Hash(request.RefreshToken);
            var stored = await uow.RefreshTokens.FindByHashAsync(hash);

            if (stored is null || stored.RevokedAt is not null || stored.ExpiresAt < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Refresh token geçersiz!");

            }

            var newPlain = tokenService.GenerateRefreshToken();

            if (!await uow.RefreshTokens.TryRevokeAsync(hash,AuthTokenIssuer.Hash(newPlain)))
            {
                throw new UnauthorizedAccessException("Refresh token geçersiz!");

            }

            await authTokenIssuer.AddRefreshTokenAsync(stored.User, newPlain);
            await uow.SaveChangesAsync();
            return authTokenIssuer.CreateResponse(stored.User, newPlain);
        }
    }
}

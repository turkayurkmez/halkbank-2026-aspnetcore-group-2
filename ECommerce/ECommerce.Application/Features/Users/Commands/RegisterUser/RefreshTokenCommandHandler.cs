using ECommerce.Application.Auth;
using ECommerce.Domain;
using ECommerce.Infrastructure.Auth;
using ECommerce.Infrastructure.UnitOfWorkPattern;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.RegisterUser
{
    public class RefreshTokenCommandHandler(IUnitOfWork uow, IPasswordHasher hasher, AuthTokenIssuer issuer) : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var role = await uow.Users.FindRoleAsync(Role.Customer) ?? new Role { Name = Role.Customer };

            var email = request.Email.Trim().ToLowerInvariant();

            var user = new User
            {
                Roles = [role],
                Email = email,
                BirthDate = request.BirthDate,
                FullName = request.FullName.Trim()
            };

            user.PasswordHash = hasher.Hash(user, request.Password);
            await uow.Users.AddAsync(user);

            var refreshPlain = await issuer.AddRefreshTokenAsync(user);

            try
            {
                await uow.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return issuer.CreateResponse(user, refreshPlain);
        }
    }
}

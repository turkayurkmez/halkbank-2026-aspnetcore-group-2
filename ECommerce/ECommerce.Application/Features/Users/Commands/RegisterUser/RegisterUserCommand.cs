using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(string Email, string Password, string FullName, DateOnly BirthDate) : IRequest<AuthResponse>;

    public record AuthResponse(string AccessToken, string RefreshToken, DateTime ExpiresAt);

    
}

using ECommerce.Application.Features.Users.Commands.RegisterUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;
    
}

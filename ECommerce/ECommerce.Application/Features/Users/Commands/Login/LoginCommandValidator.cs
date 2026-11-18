using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.Email).NotEmpty().WithMessage("Eposta zorunludur");
            RuleFor(l => l.Password).NotEmpty().WithMessage("Password zorunludur");
        }
    }
}

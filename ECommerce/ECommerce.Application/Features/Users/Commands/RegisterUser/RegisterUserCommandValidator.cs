using ECommerce.Infrastructure.UnitOfWorkPattern;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.RegisterUser
{
    //RegisterUserCommand isteği hangi durumda uygundur? Değerlendirici:
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(IUnitOfWork uow)
        {
            RuleFor(u => u.Email).NotEmpty()
                               .WithMessage("Eposta zorunludur")
                               .EmailAddress()
                               .MustAsync(async (email, _) => await uow.Users.FindByEmailAsync(email.Trim().ToLowerInvariant()) is null)
                               .WithMessage("Bu eposta ile zaten bir kullanıcı kaydı var");
        }
    }
}

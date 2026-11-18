using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.RegisterUser
{
    //Kullanıcı kayıt olmak istediğinde göndermesi gereken istek:
    public record RegisterUserCommand(string Email, string Password, string FullName, DateOnly BirthDate) : IRequest<AuthResponse>;

    //Bu istek değerlendirildikten ve eyleme gwçildikten sonra dönecek değer:
    public record AuthResponse(string AccessToken, string RefreshToken, DateTime ExpiresAt);

    
}

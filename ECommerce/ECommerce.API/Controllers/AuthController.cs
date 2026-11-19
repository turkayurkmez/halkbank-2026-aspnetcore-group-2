using ECommerce.Application.Features.Users.Commands.Login;
using ECommerce.Application.Features.Users.Commands.RefreshToken;
using ECommerce.Application.Features.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterUserCommand command) => Ok(await mediator.Send(command));

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginCommand command) => Ok(await mediator.Send(command));

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponse>>Refresh(RefreshTokenCommand command) => Ok(await mediator.Send(command));

    }
}

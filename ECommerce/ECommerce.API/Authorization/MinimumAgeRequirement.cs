using Microsoft.AspNetCore.Authorization;

namespace ECommerce.API.Authorization
{
    public record MinimumAgeRequirement(int MinimumAge) : IAuthorizationRequirement;

    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var claim = context.User.FindFirst("birthdate")?.Value;
            var birthDate = DateOnly.Parse(claim);
            var age = DateTime.Now.Year - birthDate.Year;
            if (age >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

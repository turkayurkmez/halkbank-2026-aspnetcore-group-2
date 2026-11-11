using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Extensions
{
    public static class ValidationResultExtension 
    {
        public static ActionResult ToValidationProblem(this ValidationResult result, ControllerBase controller) {

            foreach (var error in result.Errors)
            {
                controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return controller.ValidationProblem(controller.ModelState);

        }
    }
}

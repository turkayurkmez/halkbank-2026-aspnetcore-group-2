using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Behaviours
{
    /*
     * Bu class sayesinde, controller'da IValidator (örnek: ProductsController 20. satır)
     * vermek zorunda kalmayacağız. Tüm validasyon işlemleri otomatik yapılacak.
     */
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IBaseRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //eğer validatör yoksa denetleyecek bir şey yok
            if (!validators.Any())
            {
                return await next();
            }


            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = results.SelectMany(r => r.Errors).Where(f => f is not null).ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}

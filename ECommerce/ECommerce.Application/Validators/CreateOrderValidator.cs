
using ECommerce.Application.Options;
using ECommerce.Domain;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace ECommerce.Application.Validators
{
    public class CreateOrderValidator : AbstractValidator<Order>
    {
        public CreateOrderValidator(IOptions<ShopOption> options)
        {
            var maxItems = options.Value.MaxOrderItems;

            //1. Müşteri var mı yok mu?
            //2. Bir siparişte en fazla maxItems kadar ürün olabilir.

            RuleForEach(o => o.Items)
                 .NotNull()
                 .WithMessage("Siparişiniz en az bir ürün içermeli")
                 .ChildRules(item =>
                 {
                     item.RuleFor(i => i.Quantity)
                         .GreaterThan(0).WithMessage("Ürün adeti pozitif bir sayı olmalı");

                 });



        }
    }
}

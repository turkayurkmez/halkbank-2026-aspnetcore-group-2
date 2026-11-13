using FluentValidation;

namespace ECommerce.Application.Validators
{
    public static class SkuRule
    {
        public const string Pattern = "^[A-Z0-9-]{4,20}$";
        public static IRuleBuilderOptions<T,string> MustBeValidSku<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.NotEmpty().WithMessage("SKU zorunludur")
                       .Matches(Pattern)
                       .WithMessage("SKU yanlızca büyük harf, rakam ve - içerebilir ve 4-20 karakter arasında olmalıdır");
        }

    }
}

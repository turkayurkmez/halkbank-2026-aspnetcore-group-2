using ECommerce.API.Models.Requests;
using ECommerce.Infrastructure.UnitOfWorkPattern;
using FluentValidation;

namespace ECommerce.API.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.Name)
                   .NotEmpty().WithMessage("Ürün adı zorunludur")
                   .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir");


            RuleFor(c => c.Price)
                  .GreaterThan(0).WithMessage("Fiyat değeri pozitif bir sayı olmalıdır");

            RuleFor(c => c.Stock)
                 .GreaterThanOrEqualTo(0).WithMessage("Stok değeri negatif olamaz");



            var category = unitOfWork.Categories.GetByIdAsync(1);
            RuleFor(c => c.CategoryId)
                 .MustAsync(async (id, _) => await unitOfWork.Categories.GetByIdAsync(id) is not null)
                 .WithMessage(c => $"{c.CategoryId} id'li kategori bulunamadı");

            RuleFor(c => c.Sku)
                .Cascade(CascadeMode.Stop)
                .MustBeValidSku()
                .MustAsync(async (sku, _) => await unitOfWork.Products.GetProductBySku(sku) is null)
                .WithMessage("Bu isimde bir sku zaten var.");


        }
    }
}


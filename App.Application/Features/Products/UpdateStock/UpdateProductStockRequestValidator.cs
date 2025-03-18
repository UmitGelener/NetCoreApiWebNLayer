using App.Application.Features.Products.Update;
using FluentValidation;

namespace App.Application.Features.Products.UpdateStock;

public class UpdateProductStockRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductStockRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün ismi boş olmamalıdır.")
            .Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.");
                //.Must(MustUniqueProductName).WithMessage("Ürün ismi veritabanında bulunmaktadır.");

        //price validator
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır.");


        //stock validator
        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasubda olmalıdır.");
    }
}

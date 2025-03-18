using App.Application.Contracts.Persistence;
using FluentValidation;

namespace App.Application.Features.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün ismi boş olmamalıdır.")
                .Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.");
            //.Must(MustUniqueProductName).WithMessage("Ürün ismi veritabanında bulunmaktadır.");

            //price validator
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Ürün kategori değeri sıfırdan büyük olmalıdır.");

            //stock validator
            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasubda olmalıdır.");
        }
        /*
         * 1.way sync validation
        private bool MustUniqueProductName(string name)
        {
            return !_productRepository.Where(x => x.Name == name).Any();
        }
        */
    }
}

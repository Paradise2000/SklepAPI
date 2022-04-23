using FluentValidation;
using SklepAPI.Entities;

namespace SklepAPI.Models.Validators
{
    public class ProductsListNewOrderDtoValidator : AbstractValidator<ProductsListNewOrderDto>
    {
        public ProductsListNewOrderDtoValidator(DatabaseContext databaseContext)
        {
            RuleFor(r => r.productId)
                .Custom((value, context) =>
                {
                    if(!databaseContext.Products.Any(u => u.Id == value))
                    {
                        context.AddFailure($"ProductId {value} Does not exist");
                    }
                });

        }
    }
}

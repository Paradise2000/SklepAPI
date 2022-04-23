using FluentValidation;
using SklepAPI.Entities;

namespace SklepAPI.Models.Validators
{
    public class NewOrderDtoValidator : AbstractValidator<NewOrderDto>
    {
        public NewOrderDtoValidator(DatabaseContext databaseContext)
        {
            RuleFor(r => r.DeliveryOptionId)
                .Custom((value, context) =>
                {
                    if (!databaseContext.DeliveryOptions.Any(u => u.Id == value))
                    {
                        context.AddFailure($"DeliveryOptionId {value} Does not exist");
                    }
                });
            RuleForEach(x => x.ListOfProductsId).SetValidator(new ProductsListNewOrderDtoValidator(databaseContext));
        }
    }
}

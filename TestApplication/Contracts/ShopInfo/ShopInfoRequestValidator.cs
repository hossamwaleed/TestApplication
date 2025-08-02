using FluentValidation;

namespace TestApplication.Contracts.ShopInfo;

public class ShopInfoRequestValidator :AbstractValidator<ShopInfoRequest>
{
    public ShopInfoRequestValidator()
    {
        RuleFor(x=>x.Name).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.Location).NotNull().WithMessage("Location is required.");
        RuleFor(x => x.Location.lat).InclusiveBetween(-90, 90);
        RuleFor(x => x.Location.Long).InclusiveBetween(-180, 180);
    }
}

using FluentValidation;

namespace TestApplication.Contracts.ShopInfo;

public class ShopLocationRequestValidator :AbstractValidator<ShopLocationRequest>
{
    public ShopLocationRequestValidator()
    {
        RuleFor(x => x.lat).NotEmpty();
        RuleFor(x => x.Long).NotEmpty();
    }
}

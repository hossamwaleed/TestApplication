using FluentValidation;

namespace TestApplication.Contracts.Authentication;

public class LoginRequestValidator : AbstractValidator<loginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.email).NotEmpty().EmailAddress();
        RuleFor(x => x.password).NotEmpty();
    }
}

using FluentValidation;

namespace TestApplication.Contracts.Authentication;

public class RegisterRequestValidator : AbstractValidator<SignUpRequest>
{
    public RegisterRequestValidator()
    {

        RuleFor(x=>x.Email).EmailAddress().NotEmpty().WithMessage("Invalid  email."); ;
        RuleFor(x => x.password).NotEmpty();
        RuleFor(x => x.mobileNumber).NotEmpty().WithMessage("  mobile number is empty").Matches(@"^\+?[1-9]\d{1,14}$")
    .WithMessage("Invalid  mobile number.");
        RuleFor(x => x.FirstName).NotEmpty().Length(2, 35);
        RuleFor(x => x.LastName).NotEmpty().Length(2, 35);


    }
}

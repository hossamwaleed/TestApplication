using FluentValidation;
using TestApplication.Settings;

namespace TestApplication.Contracts.Product;

public class ProductRequestValidation :AbstractValidator<ProductRequest>
{
    public ProductRequestValidation()
    {
        
        RuleFor(x=>x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.price).NotEmpty();
        RuleFor(x => x.image).Must((request, context) =>
        {
            var extension = Path.GetExtension(request.image.FileName);
            return FileSetting.allowedImageExtension.Contains(extension);
        }
        ).WithMessage("file extension is not allowed").When(x=>x.image is not null);
    }
}

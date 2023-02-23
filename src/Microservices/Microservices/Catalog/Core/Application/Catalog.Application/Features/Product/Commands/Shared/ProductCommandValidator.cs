namespace Catalog.Application.Features.Product.Commands.Shared;

public class ProductCommandValidator:AbstractValidator<ProductCommand>
{
    public ProductCommandValidator()
    {
        RuleFor(pcv => pcv.Name)
            .Must(Utility.IsNotEmptyOrWhitespace)
            .WithMessage("{PropertyName} cannot be null, empty or whitespace");
        RuleFor(pcv => pcv.Category)
            .Must(Utility.IsNotEmptyOrWhitespace)
            .WithMessage("{PropertyName} cannot be null, empty or whitespace");
        RuleFor(pcv => pcv.Summary)
            .Must(Utility.IsNotEmptyOrWhitespace)
            .WithMessage("{PropertyName} cannot be null, empty or whitespace");
        RuleFor(pcv => pcv.Description)
            .Must(Utility.IsNotEmptyOrWhitespace)
            .WithMessage("{PropertyName} cannot be null, empty or whitespace");
        RuleFor(pcv => pcv.ImageFile)
            .Must(Utility.IsNotEmptyOrWhitespace)
            .WithMessage("{PropertyName} cannot be null, empty or whitespace");
        RuleFor(pcv => pcv.Price)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}");
    }
}
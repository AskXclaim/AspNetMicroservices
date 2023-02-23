namespace Catalog.Application.Features.Product.Queries.GetProductsByName;

public class GetProductsByNameQueryHandlerValidator:AbstractValidator<GetProductsByNameQuery>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByNameQueryHandlerValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        RuleFor(q => q.Name)
            .Must(Utility.IsNotEmptyOrWhitespace)
            .WithMessage("{PropertyName} cannot be null, empty or whitespace");
        RuleFor(q => q.Name)
            .MustAsync(IsNamePresent)
            .WithMessage("Invalid {PropertyValue} provided");
    }

    private async Task<bool> IsNamePresent(string categoryName, CancellationToken token) =>
        (await _productRepository.GetProductByName(categoryName)).Count > 0;
}
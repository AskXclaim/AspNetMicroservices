namespace Catalog.Application.Features.Product.Queries.GetProductsByCategoryName;

public class GetProductsByCategoryNameQueryHandlerValidator : AbstractValidator<GetProductsByCategoryNameQuery>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoryNameQueryHandlerValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        RuleFor(q => q.CategoryName)
            .NotNull().WithMessage("{PropertyName} cannot be null");
        RuleFor(q => q.CategoryName)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");
        RuleFor(q => q.CategoryName)
            .MustAsync(IsCategoryPresent)
            .WithMessage("Invalid {PropertyValue} provided");
    }

    private async Task<bool> IsCategoryPresent(string categoryName, CancellationToken token) =>
        (await _productRepository.GetProductByCategoryName(categoryName)).Count > 0;
}
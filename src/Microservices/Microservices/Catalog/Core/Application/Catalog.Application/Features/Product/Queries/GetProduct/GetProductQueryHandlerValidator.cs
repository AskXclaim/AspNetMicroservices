namespace Catalog.Application.Features.Product.Queries.GetProduct;

public class GetProductQueryHandlerValidator:AbstractValidator<GetProductsQuery>
{
    public GetProductQueryHandlerValidator()
    {
        RuleFor(q => q.Id)
            .NotNull().WithMessage("{PropertyName} cannot be null");
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");
    }
}
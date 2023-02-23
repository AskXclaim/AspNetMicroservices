namespace Catalog.Application.Features.Product.Queries.GetProduct;

public class GetProductQueryHandlerValidator:AbstractValidator<GetProductQuery>
{
    public GetProductQueryHandlerValidator()
    {
        RuleFor(q => q.Id)
            .Must(Utility.IsNotEmptyOrWhitespace)
            .WithMessage("{PropertyName} cannot be null, empty or whitespace");
    }
}
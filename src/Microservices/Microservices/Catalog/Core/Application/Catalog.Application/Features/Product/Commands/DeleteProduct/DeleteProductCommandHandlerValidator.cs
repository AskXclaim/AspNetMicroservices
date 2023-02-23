namespace Catalog.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductCommandHandlerValidator:AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandHandlerValidator()
    {
        RuleFor(dpc => dpc.Id)
            .Must(Utility.IsNotEmptyOrWhitespace)
            .WithMessage("{PropertyName} cannot be null,empty or whitespace");
    }
}
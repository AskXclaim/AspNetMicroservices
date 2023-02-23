namespace Catalog.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        Include(new ProductCommandValidator());
        RuleFor(cpc => cpc)
            .MustAsync(DoesProductExist)
            .WithMessage($"{nameof(Domain.Product)} does not exists");
    }
    private async Task<bool> DoesProductExist(UpdateProductCommand command, CancellationToken token) =>
        await _productRepository.DoesProductExist(command.Name, command.Category);
}
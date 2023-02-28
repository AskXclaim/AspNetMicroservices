namespace Catalog.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        Include(new ProductCommandValidator());
        RuleFor(cpc => cpc)
            .MustAsync(DoesProductExist)
            .WithMessage($"{nameof(Domain.Product)} already exists");
    }
    private async Task<bool> DoesProductExist(CreateProductCommand command, CancellationToken token) =>
        !(await _productRepository.DoesProductExist(command.Name, command.Category));
}
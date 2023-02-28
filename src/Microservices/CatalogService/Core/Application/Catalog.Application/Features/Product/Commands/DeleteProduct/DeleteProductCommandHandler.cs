namespace Catalog.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteProductCommandHandlerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
            throw new BadRequestException(
                $"Bad {nameof(DeleteProductCommandHandler).GetHandlerName()} request", validationResult);

        return await _productRepository.DeleteProduct(request.Id);
    }
}
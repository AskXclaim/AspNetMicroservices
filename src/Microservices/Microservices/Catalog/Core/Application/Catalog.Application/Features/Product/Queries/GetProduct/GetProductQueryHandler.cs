namespace Catalog.Application.Features.Product.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductsQuery, Domain.Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<Domain.Product> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductQueryHandlerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any()) throw new BadRequestException(validationResult);

        return await _productRepository.GetProduct(request.Id);
    }
}
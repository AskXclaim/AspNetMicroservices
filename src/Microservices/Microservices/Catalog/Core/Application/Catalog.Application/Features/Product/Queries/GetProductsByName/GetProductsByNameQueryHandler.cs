namespace Catalog.Application.Features.Product.Queries.GetProductsByName;

public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, List<Domain.Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByNameQueryHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<List<Domain.Product>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductsByNameQueryHandlerValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any()) throw new BadRequestException(validationResult);

        return await _productRepository.GetProductByName(request.Name);
    }
}
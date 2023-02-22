namespace Catalog.Application.Features.Product.Queries.GetProductsByCategoryName;

public class GetProductsByCategoryNameQueryHandler : IRequestHandler<GetProductsByCategoryNameQuery, List<Domain.Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoryNameQueryHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<List<Domain.Product>> Handle(GetProductsByCategoryNameQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductsByCategoryNameQueryHandlerValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any()) throw new BadRequestException(validationResult);

        return await _productRepository.GetProductByCategoryName(request.CategoryName);
    }
}
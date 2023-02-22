namespace Catalog.Application.Features.Product.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Domain.Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<List<Domain.Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken) =>
        await _productRepository.GetProducts();
}
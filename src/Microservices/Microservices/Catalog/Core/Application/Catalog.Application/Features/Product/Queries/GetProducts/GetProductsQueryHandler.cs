namespace Catalog.Application.Features.Product.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products=  await _productRepository.GetProducts();

        if (products == null) throw new NotFoundException($"{nameof(Domain.Product)}s not found");
        
        return _mapper.Map<List<ProductDto>>(products);
    }
}
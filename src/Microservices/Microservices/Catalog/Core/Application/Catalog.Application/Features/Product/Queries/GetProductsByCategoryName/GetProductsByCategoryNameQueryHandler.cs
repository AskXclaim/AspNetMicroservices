namespace Catalog.Application.Features.Product.Queries.GetProductsByCategoryName;

public class GetProductsByCategoryNameQueryHandler : IRequestHandler<GetProductsByCategoryNameQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsByCategoryNameQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductsByCategoryNameQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductsByCategoryNameQueryHandlerValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any()) throw new BadRequestException(validationResult);
        
        var products=  await _productRepository.GetProductByCategoryName(request.CategoryName);
        
        if (products == null) throw new NotFoundException($"{nameof(Domain.Product)}s not found");

        return _mapper.Map<List<ProductDto>>(products);
    }
}
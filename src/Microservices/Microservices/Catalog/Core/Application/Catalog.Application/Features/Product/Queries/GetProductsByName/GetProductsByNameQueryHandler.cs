namespace Catalog.Application.Features.Product.Queries.GetProductsByName;

public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsByNameQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductsByNameQueryHandlerValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any()) throw new BadRequestException(validationResult);
        
        var products=  await _productRepository.GetProductByName(request.Name);
        
        if (products == null) throw new NotFoundException($"{nameof(Domain.Product)}s not found");

        return _mapper.Map<List<ProductDto>>(products);
    }
}
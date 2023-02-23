namespace Catalog.Application.Features.Product.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductsQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductQueryHandlerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any()) throw new BadRequestException(validationResult);

        var product= await _productRepository.GetProduct(request.Id);
        
        if (product == null)
            throw new NotFoundException($"{nameof(Domain.Product)} with {nameof(request.Id)} not found");

        return _mapper.Map<ProductDto>(product);
    }
}
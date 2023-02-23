namespace Catalog.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any()) throw new BadRequestException(validationResult);

        return await _productRepository.CreateProduct(_mapper.Map<Domain.Product>(request));
    }
}
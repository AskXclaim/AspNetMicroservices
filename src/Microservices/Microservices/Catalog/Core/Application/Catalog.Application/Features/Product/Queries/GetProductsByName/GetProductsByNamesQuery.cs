namespace Catalog.Application.Features.Product.Queries.GetProductsByName;

public record GetProductsByNameQuery(string Name):IRequest<List<ProductDto>>;
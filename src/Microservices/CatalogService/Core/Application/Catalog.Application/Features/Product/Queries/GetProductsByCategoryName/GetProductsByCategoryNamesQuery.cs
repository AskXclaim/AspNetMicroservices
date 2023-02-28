namespace Catalog.Application.Features.Product.Queries.GetProductsByCategoryName;

public record GetProductsByCategoryNameQuery(string CategoryName):IRequest<List<ProductDto>>;
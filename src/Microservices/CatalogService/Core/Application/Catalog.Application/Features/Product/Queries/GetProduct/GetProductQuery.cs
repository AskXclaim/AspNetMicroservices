namespace Catalog.Application.Features.Product.Queries.GetProduct;

public record GetProductQuery(string Id):IRequest<ProductDto>;
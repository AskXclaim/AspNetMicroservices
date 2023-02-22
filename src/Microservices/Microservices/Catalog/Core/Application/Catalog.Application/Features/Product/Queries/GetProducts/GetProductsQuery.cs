namespace Catalog.Application.Features.Product.Queries.GetProducts;

public record GetProductsQuery():IRequest<List<Domain.Product>>;
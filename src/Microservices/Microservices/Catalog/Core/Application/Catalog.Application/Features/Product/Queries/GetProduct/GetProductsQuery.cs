namespace Catalog.Application.Features.Product.Queries.GetProduct;

public record GetProductsQuery(string Id):IRequest<Domain.Product>;
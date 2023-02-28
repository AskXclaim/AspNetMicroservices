namespace Catalog.Application.Features.Product.Commands.DeleteProduct;

public record DeleteProductCommand(string Id):IRequest<bool>;
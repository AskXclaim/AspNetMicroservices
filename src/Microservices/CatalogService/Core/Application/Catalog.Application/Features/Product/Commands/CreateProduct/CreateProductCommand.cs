namespace Catalog.Application.Features.Product.Commands.CreateProduct;

public record CreateProductCommand(string Name, string Category, string Summary,
        string Description, string ImageFile, decimal Price)
    :ProductCommand(Name, Category, Summary, Description, ImageFile, Price), IRequest<string>;
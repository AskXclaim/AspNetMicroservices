namespace Catalog.Application.Features.Product.Commands.UpdateProduct;

public record UpdateProductCommand(string Id,string Name, string Category, string Summary,
    string Description, string ImageFile, decimal Price) 
    :ProductCommand(Name, Category, Summary, Description, ImageFile, Price), IRequest<bool>;
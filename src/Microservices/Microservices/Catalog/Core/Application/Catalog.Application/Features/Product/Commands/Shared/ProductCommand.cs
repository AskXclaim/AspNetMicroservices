namespace Catalog.Application.Features.Product.Commands.Shared;

public record ProductCommand(string Name, string Category, string Summary, 
    string Description, string ImageFile, decimal Price);

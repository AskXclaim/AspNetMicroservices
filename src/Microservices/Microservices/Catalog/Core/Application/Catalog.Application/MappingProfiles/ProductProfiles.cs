namespace Catalog.Application.MappingProfiles;

public class ProductProfiles:Profile
{
    public ProductProfiles()
    {
        CreateMap<ProductCommand, Product>();
        CreateMap<Product, ProductDto>();
    }
}
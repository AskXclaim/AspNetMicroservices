namespace Catalog.Persistence.MappingProfiles;

public class ProductProfile:Profile
{
    public ProductProfile()
    {
        CreateMap<Entities.Product, Product>();
    }
}
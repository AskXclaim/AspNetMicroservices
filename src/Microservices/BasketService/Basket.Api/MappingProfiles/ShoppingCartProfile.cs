namespace Basket.Api.MappingProfiles;

public class ShoppingCartProfile:Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
        CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
    }
}
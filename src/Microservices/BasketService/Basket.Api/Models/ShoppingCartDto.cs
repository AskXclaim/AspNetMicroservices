namespace Basket.Api.Models;

public class ShoppingCartDto
{
    public string UserName { get; set; } = string.Empty;
    public IEnumerable<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();

    public decimal TotalPrice { get; set; }
    

    public ShoppingCartDto()
    {
    }

    public ShoppingCartDto(string userName) => UserName = userName;
}
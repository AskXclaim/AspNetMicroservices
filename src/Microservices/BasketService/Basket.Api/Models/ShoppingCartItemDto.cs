namespace Basket.Api.Models;

public class ShoppingCartItemDto
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Summary { get; set; } = string.Empty;
}
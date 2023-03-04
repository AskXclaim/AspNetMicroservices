namespace Basket.Api.Entities;

public class ShoppingCart
{
    public string UserName { get; set; } = string.Empty;
    public IEnumerable<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    public decimal TotalPrice
    {
        get { return Items.Sum(item => item.Price * item.Quantity); }
    }

    public ShoppingCart()
    {
    }

    public ShoppingCart(string userName) => UserName = userName;
}
namespace Order.Application.Features.Orders.Queries.GetOrdersList;

public class OrderDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public UserDetailsDto UserDetails { get; set; } = new();
    public AddressDto Address { get; set; } = new();

    public PaymentDetailDto PaymentDetail { get; set; } = new();
    // public string PaymentMethod { get; set; } = string.Empty;
    // public string NameOnPaymentMethod { get; set; } = string.Empty;
    // public DateOnly PaymentMethodExpirationDate { get; set; }
}
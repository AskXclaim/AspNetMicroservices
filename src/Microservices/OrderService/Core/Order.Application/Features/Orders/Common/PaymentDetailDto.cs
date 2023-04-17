namespace Order.Application.Features.Orders.Common;

public class PaymentDetailDto
{
    public string PaymentMethod { get; set; } = string.Empty;
    public string NameOnPaymentMethod { get; set; } = string.Empty;
    public DateOnly PaymentMethodExpirationDate { get; set; }
}
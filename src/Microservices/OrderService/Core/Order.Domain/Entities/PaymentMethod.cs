namespace Order.Domain.Entities;

public class PaymentMethod:EntityBase
{
    public string Name { get; set; } = string.Empty;
}
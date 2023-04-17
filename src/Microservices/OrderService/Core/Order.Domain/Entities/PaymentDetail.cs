namespace Order.Domain.Entities;

public class PaymentDetail:EntityBase
{
    //Todo change this to enum
    public int PaymentMethodId { get; set; }
    public string PaymentMethodName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public DateOnly ExpirationDate { get; set; }
    public string? CCV { get; set; } = null;
}
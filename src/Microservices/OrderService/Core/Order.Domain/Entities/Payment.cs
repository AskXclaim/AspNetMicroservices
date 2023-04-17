namespace Order.Domain.Entities;

public class Payment:EntityBase
{
    //Todo change this to enum
    public int PaymentMethod { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public DateOnly ExpirationDate { get; set; }
    public string? CCV { get; set; } = null;
}
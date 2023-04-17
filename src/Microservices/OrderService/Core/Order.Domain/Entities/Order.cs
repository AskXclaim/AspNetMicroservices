namespace Order.Domain.Entities;

public class Order:EntityBase
{
    public string UserName { get; set; }=string.Empty;
    public decimal TotalPrice { get; set; }

    public int UserDetailsId { get; set; }
    public int AddressId { get; set; }
    public int PaymentId { get; set; }
}
namespace Order.Domain.Entities;

public class Order:EntityBase
{
    public string UserName { get; set; }=string.Empty;
    public decimal TotalPrice { get; set; }
}
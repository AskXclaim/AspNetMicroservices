namespace Order.Application.Features.Orders.Common;

public class AddressDto
{
    public string AddressLineOne { get; set; } = string.Empty;
    public string AddressLineTwo { get; set; } = string.Empty;
    public string County { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostCode { get; set; } = string.Empty;
}
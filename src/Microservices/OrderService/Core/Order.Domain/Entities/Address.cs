namespace Order.Domain.Entities;

public class Address: EntityBase
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AddressLineOne { get; set; } = string.Empty;
    public string AddressLineTwo { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string County { get; set; } = string.Empty;
    public string PostCode { get; set; } = string.Empty;
}
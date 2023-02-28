namespace Catalog.Application.Features.Product.Commands.Shared;

public static class Utility
{
    public static bool IsNotEmptyOrWhitespace(string value) => !string.IsNullOrWhiteSpace(value);
}
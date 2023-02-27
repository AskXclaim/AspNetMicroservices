namespace Catalog.Application.Shared;

public static class ApplicationUtility
{
    public static string GetHandlerName(this string fullName, string partToExclude = "CommandHandler") =>
        fullName.Trim().Replace(partToExclude, "", StringComparison.OrdinalIgnoreCase);

    public static string GetErrors(ValidationResult validationResult)
    {
        var builder = new StringBuilder();
        foreach (var error in validationResult.Errors)
            builder.AppendLine(error.ErrorMessage);

        return builder.ToString();
    }
}
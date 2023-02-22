namespace Catalog.Application.Exceptions;

public class BadRequestException:Exception
{
    public string Errors { get; } = string.Empty;
    public BadRequestException(string message):base(message)
    {
        
    }

    public BadRequestException(ValidationResult validationResult):base(GetErrors(validationResult))=>
        Errors = GetErrors(validationResult);
    
    private static string GetErrors(ValidationResult validationResult)
    {
        var builder = new StringBuilder();
        foreach (var error in validationResult.Errors)
            builder.AppendLine(error.ErrorMessage);

        return builder.ToString();
    } 
}
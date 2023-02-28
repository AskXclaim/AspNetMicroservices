namespace Catalog.Application.Exceptions;

public class BadRequestException : Exception
{
    public string Errors { get; } = string.Empty;

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string title, ValidationResult validationResult)
        : base(title, new Exception(ApplicationUtility.GetErrors(validationResult))) =>
        Errors = ApplicationUtility.GetErrors(validationResult);
}
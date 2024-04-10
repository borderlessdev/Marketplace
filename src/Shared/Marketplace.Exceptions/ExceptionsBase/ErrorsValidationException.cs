namespace Marketplace.Exceptions.ExceptionsBase;

public class ErrorsValidationException : MarketplaceException
{
    public List<string> ErrorMessages {  get; set; }

    public ErrorsValidationException(List<string> errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }
}

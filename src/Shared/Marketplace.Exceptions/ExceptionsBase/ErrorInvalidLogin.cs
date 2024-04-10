namespace Marketplace.Exceptions.ExceptionsBase;

public class ErrorInvalidLogin : MarketplaceException
{
    public ErrorInvalidLogin() : base(ResourceErrorMessages.LOGIN_INVALIDO) 
    { }
}

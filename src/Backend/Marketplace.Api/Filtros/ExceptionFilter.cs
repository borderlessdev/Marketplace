using Marketplace.Communication.Response;
using Marketplace.Exceptions;
using Marketplace.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Marketplace.Api.Filtros;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MarketplaceException)
        {
            HandleException(context);
        }

        else
        {

        }
    }

    private void HandleException(ExceptionContext context)
    {
        if (context.Exception is ErrorsValidationException)
        {
            HandleValidationException(context);
        }
        if (context.Exception is ErrorInvalidLogin)
        {
            HandleLoginException(context);
        }

    }

    private void HandleLoginException(ExceptionContext context)
    {
        var loginError = context.Exception as ErrorInvalidLogin;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new ResponseErrorsJson(loginError.Message));
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var errorValidationException = context.Exception as ErrorsValidationException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        context.Result = new ObjectResult(new ResponseErrorsJson(errorValidationException.ErrorMessages));

    }

    private void UnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        context.Result = new ObjectResult(new ResponseErrorsJson(ResourceErrorMessages.ERRO_DESCONHECIDO));
    }
}

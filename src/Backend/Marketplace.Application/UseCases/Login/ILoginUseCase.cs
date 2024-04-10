using Marketplace.Communication.Request;
using Marketplace.Communication.Response;

namespace Marketplace.Application.UseCases.Login;

public interface ILoginUseCase
{
    Task<ResponseUserLoggedJson> Execute(RequestUserLoginJson request); 
}

using Marketplace.Communication.Request;
using Marketplace.Communication.Response;

namespace Marketplace.Application.UseCases.User.Signup;

public interface ISignupUseCase
{
    Task<ResponseSignupUserJson> Execute(RequestSignupUserJson request);
}

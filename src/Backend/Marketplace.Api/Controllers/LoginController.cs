using Marketplace.Application.UseCases.Login;
using Marketplace.Communication.Request;
using Marketplace.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

public class LoginController : MarketplaceController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUserLoggedJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase useCase,
        [FromBody] RequestUserLoginJson request
        )
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}

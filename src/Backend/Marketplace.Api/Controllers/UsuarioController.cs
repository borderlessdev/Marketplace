using Marketplace.Application.UseCases.User.Signup;
using Marketplace.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

public class UsuarioController : MarketplaceController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] ISignupUseCase useCase,
        [FromBody] RequestSignupUserJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);

    }

}

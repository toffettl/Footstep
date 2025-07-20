using Footstep.Application.UseCases.Users.GetByEmail;
using Footstep.Application.UseCases.Users.Register;
using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
    [HttpGet]
    [Route("{email}")]
    [ProducesResponseType(typeof(ResponseGetUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail([FromServices] IGetByEmailUserUseCase useCase, [FromRoute] string email)
    {
        var response = await useCase.Execute(email);

        return Ok(response);
    }
}

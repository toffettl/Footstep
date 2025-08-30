using Footstep.Application.UseCases.Users.GetAll;
using Footstep.Application.UseCases.Users.GetByEmail;
using Footstep.Application.UseCases.Users.GetById;
using Footstep.Application.UseCases.Users.UpdatePreferences;
using Footstep.Application.UseCases.Users.UpdateUnlockedStyles;
using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    [Route("get-by-email{email}")]
    [ProducesResponseType(typeof(ResponseGetUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail(
        [FromServices] IGetByEmailUserUseCase useCase, 
        [FromRoute] string email)
    {
        var response = await useCase.Execute(email);

        return Ok(response);
    }

    [HttpGet]
    [Route("get-by-id/{id}")]
    [ProducesResponseType(typeof(ResponseGetUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByUserId([FromServices] IGetByIdUserUseCase useCase, [FromRoute] Guid id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPut]
    [Route("preferences/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePreferences(
        [FromServices] IUpdatePreferencesUserUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] RequestUpdatePreferencesUserJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpPut]
    [Route("unlockedstyles/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUnlockedStyles(
        [FromServices] IUpdateUnlockedStylesUserUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] RequestUpdateUnlockedStylesUserJson request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetAllUserUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }
}

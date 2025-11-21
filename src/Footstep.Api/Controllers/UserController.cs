using Footstep.Application.UseCases.Users.GetAll;
using Footstep.Application.UseCases.Users.GetByEmail;
using Footstep.Application.UseCases.Users.GetById;
using Footstep.Application.UseCases.Users.GetByRanking;
using Footstep.Application.UseCases.Users.GetEmailExistence;
using Footstep.Application.UseCases.Users.UpdateAddProfilePicture;
using Footstep.Application.UseCases.Users.UpdateDeleteProfilePicture;
using Footstep.Application.UseCases.Users.UpdatePreferences;
using Footstep.Application.UseCases.Users.UpdateUnlockedStyles;
using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Users;
using Footstep.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class UserController : ControllerBase
{
    [HttpGet]
    [Route("get-by-email{email}")]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
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
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
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
    [Route("profile-picture/add/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAddProfilePicture(
        [FromServices] IUpdateAddProfilePictureUserUseCase useCase,
        [FromRoute] Guid id,
        IFormFile file)
    {
        if (file.Length == 0)
        {
            return BadRequest(ResourceErrorMessages.FILE_INVALID);
        }

        await useCase.Execute(id, file.OpenReadStream(), file.FileName, file.ContentType);

        return NoContent();
    }

    [HttpPut]
    [Route("profile-picture/remove/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDeleteProfilePicture(
        [FromServices] IUpdateDeleteProfilePictureUserUseCase useCase,
        [FromRoute] Guid id)
    {
        await useCase.Execute(id);

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
    [ProducesResponseType(typeof(ResponseUserTokenJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetAllUserUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }


    [HttpGet("pagination")]
    [ProducesResponseType(typeof(PagedResult<ResponsePaginationUserJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllPagination(
        [FromServices] IGetAllUserPaginationUseCase useCase,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var response = await useCase.Execute(page, pageSize);

        return Ok(response);
    }

    [HttpGet("ranking")]
    [ProducesResponseType(typeof(PagedResult<ResponsePaginationUserJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetByRanking(
        [FromServices] IGetUsersByRankingUseCase useCase,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] DateTime dateTime)
    {
        var response = await useCase.Execute(page, pageSize, dateTime);

        return Ok(response);
    }
}

using Footstep.Application.UseCases.RelationUser.Follow;
using Footstep.Application.UseCases.Traces.Delete;
using Footstep.Application.UseCases.UserRelation.Unfollow;
using Footstep.Application.UseCases.UsersRelation.GetAll;
using Footstep.Application.UseCases.UsersRelation.GetFollowers;
using Footstep.Application.UseCases.UsersRelation.GetFollowing;
using Footstep.Communication.Requests.UserRelation;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Traces;
using Footstep.Communication.Responses.UserRelation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.MicrosoftExtensions;

namespace Footstep.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserRelationController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreatePointOfInterestJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] IFollowUserRelationUseCase followcase,
    [FromBody] RequestUserRelationJson request)
    {
        var response = await followcase.Execute(request);

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRelation(
    [FromServices] IUnfollowUserRelationUseCase useCase,
    [FromQuery] Guid followerId,
    [FromQuery] Guid followingId)
    {
        await useCase.Execute(followerId, followingId);

        return NoContent();
    }

    [HttpGet("relations")]
    [ProducesResponseType(typeof(List<ResponseAllRelationsJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUserRelations(
        [FromServices] IGetAllUserRelationsUseCase useCase
     )

    {
        var response = await useCase.Execute();

        if (response == null)
        {
            return NoContent();
        }
        return Ok(response);
    }

    [HttpGet("followers/{followingId}")]
    [ProducesResponseType(typeof(List<ResponseFollowersJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFollowers(
        [FromServices] IGetFollowersUserRelationUseCase useCase,
        [FromRoute] Guid followingId)
    {
        var response = await useCase.Execute(followingId);

        return Ok(response);
    }

    [HttpGet("following/{followerId}")]
    [ProducesResponseType(typeof(List<ResponseFollowersJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFollowing(
    [FromServices] IGetFollowingUserRelationUseCase useCase,
    [FromRoute] Guid followerId)
    {
        var response = await useCase.Execute(followerId);

        return Ok(response);
    }
}

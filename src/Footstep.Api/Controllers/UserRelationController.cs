using Footstep.Application.UseCases.RelationUser.Follow;
using Footstep.Communication.Requests.UserRelation;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Traces;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserRelationController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreateTraceJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] IFollowUserRelationUseCase followcase,
    [FromBody] RequestUserRelationJson request)
    {
        var response = await followcase.Execute(request);

        return Ok(response);
    }
}

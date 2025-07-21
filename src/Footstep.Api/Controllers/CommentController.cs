using Footstep.Application.UseCases.Comments.Create;
using Footstep.Application.UseCases.Comments.Delete;
using Footstep.Application.UseCases.Traces.Delete;
using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Comments;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCreateComments), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromServices] ICreateCommentUseCase usecase,
        [FromBody] RequestCommentJson request)
        {
            var response = await usecase.Execute(request);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromServices] IDeleteCommentUseCase useCase,
            [FromRoute] Guid id)
        {
            await useCase.Execute(id);

            return NoContent();
        }
    }
}

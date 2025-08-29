using Footstep.Application.UseCases.Comments.Create;
using Footstep.Application.UseCases.Comments.Delete;
using Footstep.Application.UseCases.Comments.GetByAuthorId;
using Footstep.Application.UseCases.Comments.GetByParentIdAndAuthorId;
using Footstep.Application.UseCases.Comments.GetByParentsId;
using Footstep.Application.UseCases.Comments.Update;
using Footstep.Application.UseCases.Traces.Update;
using Footstep.Communication.Enums;
using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Comments;
using Footstep.Domain.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCommentJson), StatusCodes.Status201Created)]
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

        [HttpGet("by-parent/{parentId}")]
        [ProducesResponseType(typeof(List<ResponseCommentJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByParentId(
            [FromServices] IGetCommentsByParentIdUseCase useCase,
            [FromRoute] Guid parentId,
            [FromQuery] ParentType parentType)
        {
            var response = await useCase.Execute(parentId, parentType);

            return Ok(response);
        }

        [HttpGet("by-author/{id}")]
        [ProducesResponseType(typeof(List<ResponseCommentJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAuthorId(
            [FromServices] IGetCommentsByUserIdUseCase useCase,
            [FromRoute] Guid id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet("by-parent-and-author")]
        [ProducesResponseType(typeof(List<ResponseCommentJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByParentsIdAndAuthorId(
            [FromServices] IGetCommentsByParentIdAndAuthorIdUseCase useCase,
            [FromQuery] Guid parentId,
            [FromQuery] Guid authorId,
            [FromQuery] ParentType parentType)
        {
            var response = await useCase.Execute(parentId, authorId, parentType);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateStatusCommentsUseCase useCase,
            [FromRoute] Guid id,
            [FromBody] RequestUpdateStatusCommentsJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }

    }
}

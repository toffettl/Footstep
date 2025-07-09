using Footstep.Application.UseCases.Marks.Create;
using Footstep.Application.UseCases.Marks.Update;
using Footstep.Communication.Requests.Marks;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Marks;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseMarkJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromServices] ICreateMarkUseCase usecase,
            [FromBody] RequestMarkJson request)
        {
            var response = await usecase.Execute(request);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateMarkUseCase usecase,
            [FromRoute] Guid id,
            [FromBody] RequestMarkJson request)
        {
            await usecase.Execute(id, request);

            return NoContent();
        }
    }
}

using Footstep.Application.UseCases.Marks.Create;
using Footstep.Application.UseCases.Marks.Delete;
using Footstep.Application.UseCases.Marks.GetAll;
using Footstep.Application.UseCases.Marks.GetById;
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

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromServices] IDeleteMarkUseCase usecase,
            [FromRoute] Guid id)
        {
            await usecase.Execute(id);

            return NoContent();
        } 

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseMarkJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdMarkUseCase usecase,
            [FromRoute] Guid id)
        {
            var response = await usecase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseMarksJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllMarkUseCase usecase)
        {
            var response = await usecase.Execute();

            if(response.Marks.Count == 0)
            {
                return NoContent();
            }

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

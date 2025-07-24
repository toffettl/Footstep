using Footstep.Application.UseCases.Traces.Create;
using Footstep.Application.UseCases.Traces.Delete;
using Footstep.Application.UseCases.Traces.GetAll;
using Footstep.Application.UseCases.Traces.GetById;
using Footstep.Application.UseCases.Traces.GetByRay;
using Footstep.Application.UseCases.Traces.Update;
using Footstep.Application.UseCases.Traces.UpdateStatus;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Traces;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointOfInterestController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponsePointOfIntereseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromServices] ICreatePointOfInterestUseCase usecase,
            [FromBody] RequestPointOfInterestJson request)
        {
            var response = await usecase.Execute(request);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromServices] IDeletePointOfInterestUseCase useCase,
            [FromRoute] Guid id)
        {
            await useCase.Execute(id);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateStatusPointOfInterestUseCase useCase,
            [FromRoute] Guid id,
            [FromBody] RequestUpdateStatusPointOfInterestJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponsePointOfIntereseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromServices] IGetByIdPointOfInterestUseCase useCase,
            [FromRoute] Guid id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponsePointOfIntereseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllPoitntOfInterestUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Traces.Count != 0)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpGet("nearby")]
        [ProducesResponseType(typeof(List<ResponsePointOfIntereseJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNearbyTraces(
            [FromServices] IGetNearbyPointsOfInterestUseCase useCase,
            [FromQuery] double latitude,
            [FromQuery] double longitude,
            [FromQuery] double radiusInMeters)
        {
            var response = await useCase.Execute(latitude, longitude, radiusInMeters);

            return Ok(response);
        }
    }
}

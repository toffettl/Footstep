using Footstep.Application.UseCases.PointsOfInterest.UpdateDeleteImage;
using Footstep.Application.UseCases.PointsOfInterest.UpdateImages;
using Footstep.Application.UseCases.Traces.Create;
using Footstep.Application.UseCases.Traces.Delete;
using Footstep.Application.UseCases.Traces.GetAll;
using Footstep.Application.UseCases.Traces.GetAllByPage;
using Footstep.Application.UseCases.Traces.GetById;
using Footstep.Application.UseCases.Traces.GetByRay;
using Footstep.Application.UseCases.Traces.Update;
using Footstep.Application.UseCases.Traces.UpdateStatus;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Traces;
using Footstep.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PointOfInterestController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponsePointOfInterestJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromServices] ICreatePointOfInterestUseCase usecase,
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdatePointOfInterestUseCase useCase,
            [FromRoute] Guid id,
            [FromBody] RequestUpdatePointOfInterestJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }

        [HttpPut]
        [Route("Image/Add/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAddImage(
            [FromServices] IUpdateAddImagePointOfInterestUseCase useCase,
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
        [Route("Image/Remove/{imageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRemoveImage(
            [FromServices] IUpdateDeleteImagePointOfInterestUseCase useCase,
            [FromRoute] Guid imageId)
        {
            await useCase.Execute(imageId);

            return NoContent();
        }

        [HttpPut]
        [Route("Status/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Updatetatus(
            [FromServices] IUpdateStatusPointOfInterestUseCase useCase,
            [FromRoute] Guid id,
            [FromQuery] Guid userId,
            [FromQuery] bool like)
        {
            await useCase.Execute(id, userId, like);

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponsePointOfInterestJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromServices] IGetByIdPointOfInterestUseCase useCase,
            [FromRoute] Guid id,
            [FromQuery] Guid userId)
        {
            var response = await useCase.Execute(id, userId);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponsePointOfInterestJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllPoitntOfInterestUseCase useCase)
        {
            var response = await useCase.Execute();

            return Ok(response);
        }

        [HttpGet("All/Page")]
        [ProducesResponseType(typeof(ResponsePointOfInterestJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllByPage(
            [FromServices] IGetAllPointsOfInterestByPageUseCase useCase,
            [FromQuery] int page,
            [FromQuery] int pageSize)
        {
            var response = await useCase.Execute(page, pageSize);

            return Ok(response);
        }

        [HttpGet("nearby")]
        [ProducesResponseType(typeof(List<ResponsePointOfInterestJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

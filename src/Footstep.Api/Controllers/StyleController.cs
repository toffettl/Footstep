using Footstep.Application.UseCases.Styles.Create;
using Footstep.Application.UseCases.Styles.GetByName;
using Footstep.Communication.Requests.Styles;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Styles;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseStyleJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStyle(
            [FromServices] ICreateStyleUseCase useCase,
            [FromBody] RequestStyleJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

        [HttpGet("Name/{name}")]
        [ProducesResponseType(typeof(ResponseStyleJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNameStyle(
            [FromServices] IGetByNameStyleUseCase useCase,
            [FromRoute] string name)
        {
            var response = await useCase.Execute(name);

            return Ok(response);
        }
    }
}

using Footstep.Application.UseCases.Styles.Create;
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
    }
}

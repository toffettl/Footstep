using Footstep.Application.UseCases.Users.GetEmailExistence;
using Footstep.Application.UseCases.Users.Login;
using Footstep.Application.UseCases.Users.Register;
using Footstep.Communication.Requests.Users;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseUserTokenJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseUserTokenJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [FromServices] IDoLoginUseCase useCase,
            [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

        [HttpGet("email/existence/{email}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmailExistence(
        [FromServices] IGetUserEmailExistenceUseCase useCase,
        [FromRoute] string email)
        {
            var response = await useCase.Execute(email);
            return Ok(response);
        }
    }
}

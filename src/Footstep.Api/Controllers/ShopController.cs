using Footstep.Application.UseCases.Shop.GetAvailableItems;
using Footstep.Communication.Responses.Shop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        [HttpGet("items/available/{userId}")]
        [ProducesResponseType(typeof(List<ResponseShopItemJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableItemsAsync(
                [FromServices] IGetAvailableItemsUseCase useCase,
                [FromRoute] Guid userId
            )
        {
            var response = await useCase.Execute(userId);
            return Ok(response);
        }
    }
}

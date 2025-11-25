using Footstep.Application.UseCases.Shop.GetAvailableItems;
using Footstep.Application.UseCases.Shop.GetPurchasedItems;
using Footstep.Application.UseCases.Shop.GetUserCoins;
using Footstep.Application.UseCases.Shop.PurchaseItem;
using Footstep.Communication.Requests.Shop;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("items/purchased/{userId}")]
        [ProducesResponseType(typeof(List<ResponseShopItemJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPurchasedItems(
            [FromServices] IGetPurchasedItemsUseCase useCase,
            [FromRoute] Guid userId)
        {
            var response = await useCase.Execute(userId);
            return Ok(response);
        }

        [HttpGet("coins/{userId}")]
        [ProducesResponseType(typeof(ResponseUserCoinsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserCoins(
            [FromServices] IGetUserCoinsUseCase useCase,
            [FromRoute] Guid userId)
        {
            var response = await useCase.Execute(userId);
            return Ok(response);
        }

        [HttpPost("purchase")]
        [ProducesResponseType(typeof(ResponsePurchaseItemJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PurchaseItem(
            [FromServices] IPurchaseItemUseCase useCase,
            [FromBody] RequestPurchaseItemJson request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }


    }
}

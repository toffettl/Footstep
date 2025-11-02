using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Communication.Responses.Shop;

namespace Footstep.Application.UseCases.Shop.GetPurchasedItems
{
    public interface IGetPurchasedItemsUseCase
    {
        Task<List<ResponseShopItemJson>> Execute(Guid userId);
    }
}

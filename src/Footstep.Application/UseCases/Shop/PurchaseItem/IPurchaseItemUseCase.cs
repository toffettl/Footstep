using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Communication.Requests.Shop;
using Footstep.Communication.Responses.Shop;

namespace Footstep.Application.UseCases.Shop.PurchaseItem
{
    public interface IPurchaseItemUseCase
    {
        Task<ResponsePurchaseItemJson> Execute(RequestPurchaseItemJson request);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Communication.Responses.Shop
{
    public class ResponsePurchaseItemJson
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int RemainingCoins { get; set; }
        public Guid? PurchasedItemId { get; set; }

    }
}

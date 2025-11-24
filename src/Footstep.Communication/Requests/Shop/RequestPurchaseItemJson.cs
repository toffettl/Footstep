using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Communication.Requests.Shop
{
    public class RequestPurchaseItemJson
    {
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
    }
}

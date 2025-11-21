using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Communication.Responses.Shop
{
    public class ResponseShopItemJson
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string PreferenceName { get; set; }
        public string StyleName { get; set; }


    }
}

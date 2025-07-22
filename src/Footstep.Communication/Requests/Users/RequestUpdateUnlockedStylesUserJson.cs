using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Communication.Requests.Users
{
    public class RequestUpdateUnlockedStylesUserJson
    {
        public string? UnlockedMapStyles { get; set; }
        public string? UnlockedPointOfInterestStyles { get; set; }
        public string? UnlockedHeadStyles { get; set; }
        public string? UnlockedTorsoStyles { get; set; }
        public string? UnlockedLegStyles { get; set; }
        public string? UnlockedBagStyles { get; set; }
        public string? UnlockedAcessoryStyles { get; set; }
    }
}

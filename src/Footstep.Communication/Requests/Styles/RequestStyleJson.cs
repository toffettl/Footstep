using Footstep.Communication.Enums;

namespace Footstep.Communication.Requests.Styles
{
    public class RequestStyleJson
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public int? Price { get; set; }
        public bool? Store { get; set; }

        public StyleType? StyleType { get; set; }
    }
}

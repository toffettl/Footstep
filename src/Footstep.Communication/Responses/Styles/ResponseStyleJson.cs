using Footstep.Communication.Enums;

namespace Footstep.Communication.Responses.Styles
{
    public class ResponseStyleJson
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public bool Store { get; set; }

        public StyleType? StyleType { get; set; }
    }
}

using Footstep.Domain.Enums;

namespace Footstep.Communication.Requests.Traces
{
    public class RequestUpdatePointOfInterestJson
    {
        public PointOfInterestType PointOfInterestType { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}

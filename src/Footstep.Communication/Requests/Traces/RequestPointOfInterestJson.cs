using Footstep.Domain.Enums;

namespace Footstep.Communication.Requests.Traces
{
    public class RequestPointOfInterestJson
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ExpireAt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public PointOfInterestType PointOfInterestType { get; set; }
    }
}

using Footstep.Communication.Enums;

namespace Footstep.Communication.Requests.Traces
{
    public class RequestPointOfInterestJson
    {
        public RequestAuthorPointOfInterestJson? Author { get; set; }
        public PointOfInterestType PointOfInterestType { get; set; }

        public RequestCoordinates? Coordinates { get; set; }

        public RequestAddress? Address { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpireAt { get; set; }
    }

    public class RequestAuthorPointOfInterestJson
    {
        public Guid Id { get; set; }
    }

    public class RequestCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class RequestAddress
    {
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Cep { get; set; }
        public int Number { get; set; }
    }
}

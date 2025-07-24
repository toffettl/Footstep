using Footstep.Domain.Enums;

namespace Footstep.Communication.Requests.Traces
{
    public class RequestPointOfInterestJson
    {
        public Guid AuthorId { get; set; }
        public PointOfInterestType PointOfInterestType { get; set; }

        public RequestCoordinates? Coordinates { get; set; }

        public RequestAdress? Adress { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpireAt { get; set; }
    }

    public class RequestCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class RequestAdress
    {
        public string? Coutry { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Cep { get; set; }
    }
}

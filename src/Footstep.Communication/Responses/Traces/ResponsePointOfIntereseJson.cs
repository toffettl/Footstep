using Footstep.Domain.Enums;

namespace Footstep.Communication.Responses.Traces
{
    public class ResponsePointOfIntereseJson
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public PointOfInterestType PointOfInterestType { get; set; }
        
        public ResponseCoordinates? Coordinates { get; set; }
        public ResponseAdress? Adress { get; set; }
        public ResponseStatus? Status { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }
    }

    public class ResponseCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class ResponseAdress
    {
        public string? Coutry { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Cep { get; set; }
    }

    public class ResponseStatus
    {
        public int Views { get; set; }
        public int Likes { get; set; }
    }
}

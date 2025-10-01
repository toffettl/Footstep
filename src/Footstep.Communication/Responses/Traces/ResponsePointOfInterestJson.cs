using Footstep.Communication.Enums;

namespace Footstep.Communication.Responses.Traces
{
    public class ResponsePointOfInterestJson
    {
        public Guid Id { get; set; }

        public ResponseAuthor? Author { get; set; }

        public PointOfInterestType PointOfInterestType { get; set; }
        
        public ResponseCoordinates? Coordinates { get; set; }
        public ResponseAddress? Address { get; set; }
        public ResponseStatus? Status { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }
    }

    public class ResponseAuthor
    {
        public Guid AuthorId { get; set; }
        public string? Name { get; set; }
        public ResponsePointOfInterestAvatarStyle? AvatarStyle { get; set; }
    }

    public class ResponsePointOfInterestAvatarStyle
    {
        public Guid Head {  get; set; }
        public Guid Torso { get; set; }
        public Guid Leg { get; set; }
        public Guid Bag { get; set; }
        public Guid Accessory { get; set; }
    }

    public class ResponseCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class ResponseAddress
    {
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Cep { get; set; }
        public int Number { get; set; }
    }

    public class ResponseStatus
    {
        public int Views { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
    }
}

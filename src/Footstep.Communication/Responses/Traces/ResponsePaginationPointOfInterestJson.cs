using Footstep.Communication.Enums;

namespace Footstep.Communication.Responses.Traces
{
    public class ResponsePaginationPointOfInterestJson
    {
        public Guid Id { get; set; }

        public ResponsePaginationAuthor? Author { get; set; }

        public PointOfInterestType PointOfInterestType { get; set; }

        public ResponsePaginationCoordinates? Coordinates { get; set; }
        public ResponsePaginationAddress? Address { get; set; }
        public ResponsePaginationStatus? Status { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

    public class ResponsePaginationAuthor
    {
        public Guid AuthorId { get; set; }
        public string? Name { get; set; }
        public ResponsePaginationAvatarStyle? AvatarStyle { get; set; }
    }

    public class ResponsePaginationAvatarStyle
    {
        public Guid Head { get; set; }
        public Guid Body { get; set; }
        public Guid Leg { get; set; }
        public Guid Bag { get; set; }
        public Guid Accessory { get; set; }
    }

    public class ResponsePaginationCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class ResponsePaginationAddress
    {
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Cep { get; set; }
        public int Number { get; set; }
    }

    public class ResponsePaginationStatus
    {
        public int Views { get; set; }
        public int Likes { get; set; }
        public int Commentaries { get; set; }
    }
}

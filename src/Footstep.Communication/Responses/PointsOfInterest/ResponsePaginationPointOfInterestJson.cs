using Footstep.Communication.Enums;

namespace Footstep.Communication.Responses.Traces
{
    public class ResponsePaginationPointOfInterestJson
    {
        public Guid Id { get; set; }
        public ResponsePaginationPointOfInterestAuthorJson? Author { get; set; }
        public PointOfInterestType POIType { get; set; }
        public string? Style { get; set; }
        public ResponsePaginationPointOfInterestCoordinatesJson? Coordinates { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ResponsePaginationPointOfInterestMediaJson Media { get; set; } = new ResponsePaginationPointOfInterestMediaJson();
        public ResponsePaginationPointOfInterestStatusJson? Status { get; set; }
        public ResponsePaginationPointOfInterestAddressJson? Address { get; set; }
        public DateTime? ExpireAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ResponsePaginationPointOfInterestAuthorJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool AvatarOverProfile { get; set; }
        public ResponsePaginationPointOfInterestCharacterStyleJson? Avatar { get; set; }
        public ResponsePaginationPointOfInterestProfilePictureJson? ProfilePicture { get; set; }
    }

    public class ResponsePaginationPointOfInterestCharacterStyleJson
    {
        public string? Skin { get; set; }
        public ResponsePaginationPointOfInterestItemJson? Top { get; set; }
        public ResponsePaginationPointOfInterestItemJson? Backpack { get; set; }
        public ResponsePaginationPointOfInterestItemJson? Clothe { get; set; }
        public string? Eye { get; set; }
        public string? Eyebrow { get; set; }
        public string? Mouth { get; set; }
        public ResponsePaginationPointOfInterestItemJson? FacialHair { get; set; }
        public ResponsePaginationPointOfInterestItemJson? Accessory { get; set; }
    }

    public class ResponsePaginationPointOfInterestItemJson
    {
        public string? Style { get; set; }
        public string? Color { get; set; }
    }

    public class ResponsePaginationPointOfInterestProfilePictureJson
    {
        public string? Uri { get; set; }
        public string? Style { get; set; }
    }
    //End of Author

    public class ResponsePaginationPointOfInterestCoordinatesJson
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class ResponsePaginationPointOfInterestMediaJson
    {
        public ResponsePaginationPointOfInterestImageJson? Image { get; set; } 
    }

    public class ResponsePaginationPointOfInterestImageJson
    {
        public Guid Id { get; set; }
        public string? Url { get; set; }
    }

    public class ResponsePaginationPointOfInterestStatusJson
    {
        public int Views { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
    }

    public class ResponsePaginationPointOfInterestAddressJson
    {
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Cep { get; set; }
        public string? Number { get; set; }
    }
}

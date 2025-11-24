using Footstep.Communication.Enums;

namespace Footstep.Communication.Responses.Traces
{
    public class ResponsePointOfInterestJson
    {
        public Guid Id { get; set; }
        public ResponsePointOfInterestAuthorJson? Author { get; set; }
        public PointOfInterestType POIType { get; set; }
        public string? Style { get; set; }
        public ResponsePointOfInterestCoordinatesJson? Coordinates { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? Tags { get; set; }
        public ResponsePointOfInterestMediaJson Media { get; set; } = new ResponsePointOfInterestMediaJson();
        public ResponsePointOfInterestStatusJson? Status { get; set; }
        public ResponsePointOfInterestAddressJson? Address { get; set; }
        public DateTime? ExpireAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    //Start of Author
    public class ResponsePointOfInterestAuthorJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool AvatarOverProfile { get; set; }
        public ResponsePointOfInterestCharacterStyleJson? Avatar { get; set; }
        public ResponsePointOfInterestProfilePictureJson? ProfilePicture { get; set; }
    }

    public class ResponsePointOfInterestCharacterStyleJson
    {
        public string? Skin { get; set; }
        public ResponsePointOfInterestItemJson? Top { get; set; }
        public ResponsePointOfInterestItemJson? Backpack { get; set; }
        public ResponsePointOfInterestItemJson? Clothe { get; set; }
        public string? Eye { get; set; }
        public string? Eyebrow { get; set; }
        public string? Mouth { get; set; }
        public ResponsePointOfInterestItemJson? FacialHair { get; set; }
        public ResponsePointOfInterestItemJson? Accessory { get; set; }
    }

    public class ResponsePointOfInterestItemJson
    {
        public string? Style { get; set; }
        public string? Color { get; set; }
    }

    public class ResponsePointOfInterestProfilePictureJson
    {
        public string? Uri { get; set; }
        public string? Style { get; set; }
    }
    //End of Author

    public class ResponsePointOfInterestCoordinatesJson
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class ResponsePointOfInterestMediaJson     {
        public List<ResponsePointOfInterestImageJson> Images { get; set; } = new List<ResponsePointOfInterestImageJson>();
        public List<string> Videos { get; set; } = new List<string>();
    }

    public class ResponsePointOfInterestImageJson
    {
        public Guid Id { get; set; }
        public string? Url { get; set; }
    }

    public class ResponsePointOfInterestStatusJson
    {
        public int Views { get; set; }
        public int Likes { get; set; }
        public List<Guid>? Comments { get; set; }
    }

    public class ResponsePointOfInterestAddressJson
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

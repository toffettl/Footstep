namespace Footstep.Communication.Responses.Users
{
    public class ResponsePaginationUserJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public ResponsePaginationPreferences? Preferences { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ResponsePaginationPreferences
    {
        public bool? AvatarOverProfile { get; set; }
        public ResponsePaginationAvatarStyle? AvatarStyle { get; set; }
    }

    public class ResponsePaginationAvatarStyle
    {
        public string? Head { get; set; }
        public string? Body { get; set; }
        public string? Leg { get; set; }
        public string? Bag { get; set; }
        public string? Accessory { get; set; }
    }
}

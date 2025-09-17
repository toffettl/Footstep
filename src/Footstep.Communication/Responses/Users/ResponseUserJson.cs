namespace Footstep.Communication.Responses.Users
{
    public class ResponseUserJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Biography { get; set; }
        public ResponsePreferences? Preferences { get; set; } 
        public ResponseUnlockedStyles? UnlockedStyles { get; set; } = new ResponseUnlockedStyles();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ResponsePreferences
    {
        public string? MapStyle { get; set; }
        public string? PointOfInterestStyle { get; set; }
        public bool? AvatarOverProfile { get; set; }
        public ResponseAvatarStyle? AvatarStyle { get; set; }
    }

    public class ResponseAvatarStyle
    {
        public string? Head { get; set; }
        public string? Body { get; set; }
        public string? Leg { get; set; }
        public string? Bag { get; set; }
        public string? Accessory { get; set; }
    }

    public class ResponseUnlockedStyles
    {
        public string? UnlockedMapStyles { get; set; }
        public List<string>? UnlockedPointOfInterestStyles { get; set; }
        public List<string>? UnlockedHeadStyles { get; set; }
        public List<string>? UnlockedTorsoStyles { get; set; }
        public List<string>? UnlockedLegStyles { get; set; }
        public List<string>? UnlockedBagStyles { get; set; }
        public List<string>? UnlockedAcessoryStyles { get; set; }
    }
}

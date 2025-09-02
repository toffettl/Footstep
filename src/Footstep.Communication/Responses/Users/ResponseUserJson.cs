namespace Footstep.Communication.Responses.Users
{
    public class ResponseUserJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Biography { get; set; }
        public ResponsePreferencesJson? Preferences { get; set; } 
        public ResponseUnlockedStylesJson? UnlockedStyles { get; set; } = new ResponseUnlockedStylesJson();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ResponsePreferencesJson
    {
        public string? MapStyle { get; set; }
        public Guid? PointOfInterestStyle { get; set; }
        public bool? AvatarOverProfile { get; set; }
        public ResponseAvatarStyleJson? AvatarStyle { get; set; } = new ResponseAvatarStyleJson();
    }

    public class ResponseAvatarStyleJson
    {
        public Guid? Head { get; set; }
        public Guid? Body { get; set; }
        public Guid? Leg { get; set; }
        public Guid? Bag { get; set; }
        public Guid? Acessory { get; set; }
    }

    public class ResponseUnlockedStylesJson
    {
        public string? UnlockedMapStyles { get; set; }
        public List<Guid>? UnlockedPointOfInterestStyles { get; set; }
        public List<Guid>? UnlockedHeadStyles { get; set; }
        public List<Guid>? UnlockedBodyStyles { get; set; }
        public List<Guid>? UnlockedLegStyles { get; set; }
        public List<Guid>? UnlockedBagStyles { get; set; }
        public List<Guid>? UnlockedAcessoryStyles { get; set; }
    }
}

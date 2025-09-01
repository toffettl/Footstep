namespace Footstep.Communication.Responses.Users
{
    public class ResponseUserJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Biography { get; set; }
        public Preferences? Preferences { get; set; }
        public UnlockedStyles? UnlockedStyles { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class Preferences
    {
        public string? MapStyle { get; set; }
        public Guid? PointOfInterestStyle { get; set; }
        public bool? AvatarOverProfile { get; set; }
        public AvatarStyle? AvatarStyle { get; set; }
    }

    public class AvatarStyle
    {
        public Guid? Head { get; set; }
        public Guid? Torso { get; set; }
        public Guid? Leg { get; set; }
        public Guid? Bag { get; set; }
        public Guid? Acessory { get; set; }
    }

    public class UnlockedStyles
    {
        public List<string>? UnlockedMapStyles { get; set; }
        public List<Guid>? UnlockedPointOfInterestStyles { get; set; }
        public List<Guid>? UnlockedHeadStyles { get; set; }
        public List<Guid>? UnlockedTorsoStyles { get; set; }
        public List<Guid>? UnlockedLegStyles { get; set; }
        public List<Guid>? UnlockedBagStyles { get; set; }
        public List<Guid>? UnlockedAcessoryStyles { get; set; }
    }
}

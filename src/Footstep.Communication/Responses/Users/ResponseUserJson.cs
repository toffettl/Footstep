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
        public Guid? Accessories { get; set; }
        public Guid? Top { get; set; }
        public Guid? FacialHair { get; set; }
        public Guid? Clothes { get; set; }
        public Guid? Eyes { get; set; }
        public Guid? Eyebrown { get; set; }
        public Guid? Mouth { get; set; }
        public Guid? Skin { get; set; }
        public Guid? BackPack { get; set; }
    }

    public class ResponseUnlockedStylesJson
    {
        public string? UnlockedMapStyles { get; set; }
        public List<Guid>? UnlockedPointOfInterestStyles { get; set; }
        public List<Guid>? UnlockedAccessoriesStyles { get; set; }
        public List<Guid>? UnlockedTopStyles { get; set; }
        public List<Guid>? UnlockedFacialHairStyles { get; set; }
        public List<Guid>? UnlockedClothesStyles { get; set; }
        public List<Guid>? UnlockedEyesStyles { get; set; }
        public List<Guid>? UnlockedEyebrownStyles { get; set; }
        public List<Guid>?  UnlockedMouthStyles { get; set; }
        public List<Guid>? UnlockedSkinStyles { get; set; }
        public List<Guid>? UnlockedBackPackStyles { get; set; }

    }
}

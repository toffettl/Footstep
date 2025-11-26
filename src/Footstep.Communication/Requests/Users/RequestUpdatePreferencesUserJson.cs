namespace Footstep.Communication.Requests.Users
{
    public class RequestUpdatePreferencesUserJson
    {
        public string? MapStyle { get; set; }
        public Guid? PointOfInterestStyle { get; set; }
        public bool? AvatarOverProfile { get; set; }
        public RequestAvatarStyleJson? AvatarStyle { get; set; }
    }

    public class RequestAvatarStyleJson
    {
        public Guid? Skin { get; set; }
        public Guid? Top { get; set; }
        public Guid? Backpack { get; set; }
        public Guid? Clothe { get; set; }
        public Guid? Eye { get; set; }
        public Guid? Eyebrown { get; set; }
        public Guid? Mouth { get; set; }
        public Guid? FacialHair { get; set; }

        public Guid? Acessory { get; set; }
    }
}

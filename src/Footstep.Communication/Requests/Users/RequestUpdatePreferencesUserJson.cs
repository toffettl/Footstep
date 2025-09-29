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
        public Guid? Head { get; set; }
        public Guid? Torso { get; set; }
        public Guid? Leg { get; set; }
        public Guid? Bag { get; set; }
        public Guid? Acessory { get; set; }
    }
}

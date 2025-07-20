namespace Footstep.Communication.Responses.Users
{
    public class Preferences
    {
        public string? MapStyle { get; set; }
        public string? PointOfInterestStyle { get; set; }
        public bool? AvatarOverProfile { get; set; }
        public AvatarStyle AvatarStyle { get; set; }
    }
}

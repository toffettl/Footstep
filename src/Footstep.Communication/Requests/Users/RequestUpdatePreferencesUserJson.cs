namespace Footstep.Communication.Requests.Users
{
    public class RequestUpdatePreferencesUserJson
    {
        public string? ProfileStyle { get; set; }
        public string? MapStyle { get; set; }
        public string? PointOfInterestStyle { get; set; }
        public bool? AvatarOverProfile { get; set; }
        public RequestAvatarStyleJson? AvatarStyle { get; set; }
    }
}

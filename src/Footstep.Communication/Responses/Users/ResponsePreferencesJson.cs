namespace Footstep.Communication.Responses.Users
{
    public class ResponsePreferencesJson
    {
        public string? MapStyle { get; set; }
        public string? PointOfInterestStyle { get; set; }
        public bool? AvatarOverProfile { get; set; }
        public ResponseAvatarStyleJson? AvatarStyle { get; set; }
    }
}

namespace Footstep.Communication.Responses.Users
{
    public class ResponsePaginationUserJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ResponsePaginationUserProfilePictureJson? ProfilePicture { get; set; }
        public ResponsePaginationUserSocialJson? Social { get; set; }
        public ResponsePaginationUserPreferencesJson? Preferences { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ResponsePaginationUserProfilePictureJson
    {
        public string? Uri { get; set; }
        public string? Style { get; set; }
    }

    public class ResponsePaginationUserSocialJson
    {
        public List<Guid>? Followers { get; set; }
        public List<Guid>? Following { get; set; }
    }

    //Start of Preferences
    public class ResponsePaginationUserPreferencesJson
    {
        public bool? AvatarOverProfile { get; set; }
        public ResponsePaginationUserCharacterStyleJson? Avatar { get; set; }
    }

    public class ResponsePaginationUserCharacterStyleJson
    {
        public string? Skin { get; set; }
        public ResponsePaginationUserItemJson? Top { get; set; }
        public ResponsePaginationUserItemJson? Backpack { get; set; }
        public ResponsePaginationUserItemJson? Clothe { get; set; }
        public string? Eye { get; set; }
        public string? Eyebrow { get; set; }
        public string? Mouth { get; set; }
        public ResponsePaginationUserItemJson? FacialHair { get; set; }
        public ResponsePaginationUserItemJson? Accessory { get; set; }
    }

    public class ResponsePaginationUserItemJson
    {
        public string? Style { get; set; }
        public string? Color { get; set; }
    }
    //End of Preferences
}

using System.Diagnostics;

namespace Footstep.Communication.Responses.Users
{
    public class ResponseUserJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Biography { get; set; }
        public ResponseUserProfilePictureJson ProfilePicture { get; set; } = new ResponseUserProfilePictureJson();
        public ResponseUserSocialJson? Social { get; set; }
        public ResponseUserActivityJson? Activity { get; set; }
        public ResponseUserPreferencesJson? Preferences { get; set; }
        public ResponseUserUnlockedStyles? UnlockedStyles { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ResponseUserProfilePictureJson
    {
        public string? Uri { get; set; }
        public string? Style { get; set; }
    }

    public class ResponseUserSocialJson
    {
        public List<Guid>? Followers { get; set; }
        public List<Guid>? Following { get; set; }
        public List<Guid>? Comments { get; set; }
        public List<Guid>? Likes { get; set; }
    }

    //Start of Activity
    public class ResponseUserActivityJson
    {
        public ResponseUserPOIsJson? POIs { get; set; }
        public ResponseUserCoinJson? Coins { get; set; }
    } 

    public class ResponseUserPOIsJson
    {
        public List<Guid>? Steps { get; set; }
        public List<Guid>? Marks { get; set; }
    }

    public class ResponseUserCoinJson
    {
        public int Total { get; set; }
        public int Spent { get; set; }
        public int Earned { get; set; }
    }
    //End of Activity

    //Start of Preferences
    public class ResponseUserPreferencesJson
    {
        public string? Map { get; set; }
        public string? POI { get; set; }
        public bool? AvatarOverProfile { get; set; }
        public ResponseUserCharacterStyleJson? Avatar { get; set; }
    }

    public class ResponseUserCharacterStyleJson
    {
        public string? Skin { get; set; }
        public ResponseUserItemJson? Top { get; set; }
        public ResponseUserItemJson? Backpack { get; set; }
        public ResponseUserItemJson? Clothe { get; set; }
        public string? Eye { get; set; }
        public string? Eyebrow { get; set; }
        public string? Mouth { get; set; }
        public ResponseUserItemJson? FacialHair { get; set; }
        public ResponseUserItemJson? Accessory { get; set; }
    }
    //End of Preferences

    //Start of UnlockedStyles
    public class ResponseUserUnlockedStyles
    {
        public List<string>? Map { get; set; }
        public List<string>? POI { get; set; }
        public ResponseUserCharacterStylesJson? Avatar { get; set; }
    }

    public class ResponseUserCharacterStylesJson
    {
        public List<string>? Skin { get; set; }
        public List<ResponseUserItemJson>? Top { get; set; }
        public List<ResponseUserItemJson>? Backpack { get; set; }
        public List<ResponseUserItemJson>? Clothe { get; set; }
        public List<string>? Eye { get; set; }
        public List<string>? Eyebrow { get; set; }
        public List<string>? Mouth { get; set; }
        public List<ResponseUserItemJson>? FacialHair { get; set; }
        public List<ResponseUserItemJson>? Accessory { get; set; }
    }
    //End of UnlockedStyles

    public class ResponseUserItemJson
    {
        public string? Style { get; set; }
        public string? Color { get; set; }
    }
}

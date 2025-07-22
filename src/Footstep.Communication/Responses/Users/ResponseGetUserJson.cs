namespace Footstep.Communication.Responses.Users
{
    public class ResponseGetUserJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Biography { get; set; }
        public ResponsePreferencesJson Preferences { get; set; }
        public ResponseUnlockedStylesJson UnlockedStyles { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

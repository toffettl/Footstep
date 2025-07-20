namespace Footstep.Communication.Responses.Users
{
    public class ResponseGetUserJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Biography { get; set; }
        public Preferences Preferences { get; set; }
        public UnlockedStyles UnlockedStyles { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

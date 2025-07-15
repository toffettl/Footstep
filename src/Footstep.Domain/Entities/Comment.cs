namespace Footstep.Domain.Entities;
public class Comment
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }
    public Guid TargetUserId { get; set; }

    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User Author { get; set; } = null!;
    public User TargetUser { get; set; } = null!;
}

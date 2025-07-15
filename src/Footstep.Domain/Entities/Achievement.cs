namespace Footstep.Domain.Entities;
public class Achievement
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}
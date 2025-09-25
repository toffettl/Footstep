namespace Footstep.Domain.Entities;
public class Followership
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; } 

    public User? Follower { get; set; } = null!;
    public User? Following { get; set; } = null!;
}

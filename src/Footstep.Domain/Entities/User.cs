namespace Footstep.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Biography { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<UserRelation> Followers { get; set; } = new List<UserRelation>();
    public ICollection<UserRelation> Following { get; set; } = new List<UserRelation>();

    public ICollection<LikeComment> LikeComments { get; set; } = new List<LikeComment>();
}

namespace Footstep.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public ICollection<UserRelation> Followers { get; set; } = new List<UserRelation>();
    public ICollection<UserRelation> Following { get; set; } = new List<UserRelation>();

    public ICollection<Comment> CommentsReceived { get; set; } = new List<Comment>();
    public ICollection<Comment> CommentsWritten { get; set; } = new List<Comment>();

    public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
}

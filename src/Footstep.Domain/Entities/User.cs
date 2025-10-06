namespace Footstep.Domain.Entities;
public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Biography { get; set; } = "";
    public string? Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Guid CoinId { get; set; }
    public Guid PreferenceId { get; set; }

    public Coin Coin { get; set; } = new Coin();
    public Preference Preference { get; set; } = new Preference();

    public ICollection<Followership> Followers { get; set; } = new List<Followership>();
    public ICollection<Followership> Following { get; set; } = new List<Followership>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<CommentLike> LikeComments { get; set; } = new List<CommentLike>();
    public ICollection<PointOfInterest> PointsOfInterest {  get; set; } = new List<PointOfInterest>();
    public ICollection<UserPointOfInterestRelation> UserPointOfInterestRelations { get; set; } = new List<UserPointOfInterestRelation>();
}

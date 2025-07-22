namespace Footstep.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Biography { get; set; }
    public string? Password { get; set; }
    public string? MapStyle { get; set; }
    public string? PointOfInterestStyle { get; set; }
    public bool? AvatarOverProfile { get; set; }
    public string? HeadStyle { get; set; }
    public string? TorsoStyle { get; set; }
    public string? LegStyle { get; set; }
    public string? BagStyle { get; set; }
    public string? AcessoryStyle {  get; set; }
    public string? UnlockedMapStyles { get; set; }
    public string? UnlockedPointOfInterestStyles { get; set; }
    public string? UnlockedHeadStyles { get; set; }
    public string? UnlockedTorsoStyles { get; set; }
    public string? UnlockedLegStyles { get; set; }
    public string? UnlockedBagStyles { get; set; }
    public string? UnlockedAcessoryStyles { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

namespace Footstep.Domain.Entities
{
    public class Preference
    { 
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? MapStyle { get; set; }
        public bool AvatarOverProfile { get; set; } = true;

        public Guid UserId { get; set; }

        public User? User { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}

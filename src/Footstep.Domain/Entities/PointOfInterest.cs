using System.Security.Cryptography;
using Footstep.Domain.Enums;

namespace Footstep.Domain.Entities
{
    public class PointOfInterest
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        public PointOfInterestType PointOfInterestType { get; set; }
        public PointOfInterestVisibility PointOfInterestVisibility { get; set; }

        public DateTime? ExpireAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Guid AddressId { get; set; }

        public Address? Address { get; set; }

        public ICollection<UserPointOfInterestRelation> UserPointOfInterestRelations { get; set; } = new List<UserPointOfInterestRelation>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

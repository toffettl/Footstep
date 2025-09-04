using Footstep.Domain.Enums;

namespace Footstep.Domain.Entities
{
    public class UserPointOfInterestRelation
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Like { get; set; } = false;

        public UserPointOfInterestRelationType Type { get; set; }

        public Guid UserId { get; set; }
        public Guid PointOfInterestId { get; set; }

        public User? User { get; set; }
        public PointOfInterest? PointOfInterest { get; set; }
    }
}

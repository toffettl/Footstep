using Footstep.Domain.Enums;

namespace Footstep.Domain.Entities
{
    public class UserPointOfInterestRelation
    {
        public Guid Id { get; set; }

        public int Likes { get; set; }

        public UserPointOfInterestRelationType Type { get; set; }

        public User? User { get; set; }
        public Guid UserId { get; set; }

        public PointOfInterest? PointOfInterest { get; set; }
        public Guid PointOfInterestId { get; set; }
    }
}

using System.Security.Cryptography;
using Footstep.Domain.Enums;

namespace Footstep.Domain.Entities
{
    public class PointOfInterest
    {
        public Guid Id { get; set; }
        public PointOfInterestType PointOfInterestType { get; set; }
        public PointOfInterestVisibility PointOfInterestVisibility { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}

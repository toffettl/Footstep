namespace Footstep.Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Url { get; set; }

        public Guid PointOfInterestId { get; set; }

        public PointOfInterest? PointOfInterest { get; set; }
    }
}

namespace Footstep.Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? PreferenceId { get; set; }
        public Guid? PointOfInterestId { get; set; }

        public Preference? Preference { get; set; }
        public PointOfInterest? PointOfInterest { get; set; }
    }
}

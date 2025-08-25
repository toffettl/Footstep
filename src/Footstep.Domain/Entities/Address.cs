namespace Footstep.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Cep { get; set; }
        public int Number { get; set; }

        public ICollection<PointOfInterest> PointOfInterests { get; set; } = new List<PointOfInterest>();
    }
}

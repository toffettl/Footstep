namespace Footstep.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Cep { get; set; }

        public int number { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public PointOfInterest? PointOfInterest { get; set; }
        public Guid PointOfInterestId { get; set; }
    }
}

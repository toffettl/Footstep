using System.Security.Cryptography;
using Footstep.Domain.Enums;

namespace Footstep.Domain.Entities
{
    public class PointOfInterest
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public PointOfInterestType PointOfInterestType { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string? Coutry { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Cep { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        public int Views { get; set; }
        public int Likes { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }

        public User? User { get; set; }
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
    }
}

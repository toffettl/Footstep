using Footstep.Domain.Enums;

namespace Footstep.Domain.Entities
{
    public class Style
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public StyleType StyleType { get; set; }

        public ICollection<User> UnlockedUsers { get; set; } = new List<User>();
        public ICollection<User> ActiveUsers { get; set; } = new List<User>();
    }
}

using Footstep.Domain.Enums;

namespace Footstep.Domain.Entities
{
    public class Style
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Name { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public bool Store { get; set; }

        public StyleType StyleType { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}

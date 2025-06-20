namespace Footstep.Domain.Entities
{
    public class Trace
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ExpireAt { get; set; }
        public int Like { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

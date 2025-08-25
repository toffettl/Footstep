namespace Footstep.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Unblocked { get; set; }
        public bool Equipped { get; set; } = false;

        public Guid PreferenceId { get; set; }
        public Guid StyleId { get; set; }

        public Preference? Preference { get; set; }
        public Style? Style { get; set; }
    }
}

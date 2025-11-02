namespace Footstep.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Unlocked { get; set; }
        public bool Equipped { get; set; } = false;
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvaliableInShop { get; set; } = true;

        public Guid PreferenceId { get; set; }
        public Guid StyleId { get; set; }

        public Preference? Preference { get; set; }
        public Style? Style { get; set; }
        public ICollection<UserItem> UserItems { get; set; } = new List<UserItem>();
    }
}

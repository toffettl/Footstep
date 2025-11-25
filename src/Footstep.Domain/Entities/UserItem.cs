using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Domain.Entities
{
    public class UserItem
    {
        public Guid UserItemId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
        public Item? Item { get; set; }

    }
}

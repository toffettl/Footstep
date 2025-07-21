using Footstep.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Footstep.Domain.Entities
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }

        public Guid ParentId;
        public ParentType ParentType { get; set; }

        public string? Content { get; set; }

        public int Likes { get; set; }
        public int Replies { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

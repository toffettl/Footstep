using Footstep.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Footstep.Domain.Entities
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        public string? Content { get; set; }

        public ParentType ParentType { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid AuthorId { get; set; }
        public Guid CommentId { get; set; }
        public Guid PointOfInterestId { get; set; }

        public User? User { get; set; }
        public PointOfInterest? PointOfInterest { get; set; }


        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<LikeComment> LikesComments { get; set; } = new List<LikeComment>();
    }
}

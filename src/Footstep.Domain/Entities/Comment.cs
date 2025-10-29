using Footstep.Domain.Enums;

namespace Footstep.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Content { get; set; }

        public ParentType ParentType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public Guid? ParentPointOfInterestId { get; set; }
        public Guid? ParentCommentId { get; set; }

        public User? User { get; set; }
        public PointOfInterest? ParentPointOfInterest { get; set; }
        public Comment? ParentComment { get; set; }


        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();
    }
}

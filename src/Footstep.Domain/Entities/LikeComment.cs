namespace Footstep.Domain.Entities
{
    public class LikeComment
    {
        public Guid Id { get; set; }

        public User? User { get; set; }
        public Guid UserId { get; set; }

        public Comment? Comment { get; set; }
        public Guid CommentId { get; set; }
    }
}

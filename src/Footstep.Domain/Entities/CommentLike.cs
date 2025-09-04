namespace Footstep.Domain.Entities
{
    public class CommentLike
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }

        public User? User { get; set; }
        public Comment? Comment { get; set; }
    }
}

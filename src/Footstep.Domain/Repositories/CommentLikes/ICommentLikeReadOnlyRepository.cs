using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.CommentLikes
{
    public interface ICommentLikeReadOnlyRepository
    {
        Task<bool> ExistCommentWithUserIdAndCommentId(Guid userId, Guid commentId);
    }
}

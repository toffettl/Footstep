using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.CommentLikes
{
    public interface ICommentLikeWriteOnlyRepository
    {
        Task Add(CommentLike commentLike);
    }
}

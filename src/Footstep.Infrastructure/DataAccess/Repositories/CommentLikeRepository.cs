using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.CommentLikes;
using Footstep.Domain.Repositories.Comments;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    internal class CommentLikeRepository : ICommentLikeWriteOnlyRepository, ICommentLikeReadOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;

        public CommentLikeRepository(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(CommentLike commentLike)
        {
            await _dbContext.CommentLikes.AddAsync(commentLike);
        }

        public async Task<bool> ExistCommentWithUserIdAndCommentId(Guid userId, Guid commentId)
        {
            return await _dbContext.CommentLikes.AnyAsync(cl => cl.UserId == userId && cl.CommentId == commentId);
        }
    }
}

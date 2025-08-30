using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Comments;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class CommentRepository : ICommentsWriteOnlyRepository,
        ICommentsReadOnlyRepository,
        ICommentsUpdateOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;
        public CommentRepository(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
        }

        public async Task<bool?> Delete(Guid id)
        {
            var result = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (result == null)
            {
                return false;
            }

            _dbContext.Comments.Remove(result);

            return true;
        }

        public async Task<List<Comment>> GetByUserId(Guid id)
        {
            return await _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsNoTracking()
                .Where(c => c.UserId == id)
                .ToListAsync();
        }

        public async Task<Comment?> GetById(Guid id)
        {
            return await _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Comment>> GetByPointOfInterestIdAndAuthorId(Guid pointOfInterestId, Guid authorId)
        {
            return await _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)  
                .AsNoTracking()
                .Where(c => c.ParentPointOfInterestId == pointOfInterestId && c.UserId == authorId)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetByPointOfInterestId(Guid id)
        {
            return await _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsNoTracking()
                .Where(c => c.ParentPointOfInterestId == id)
                .ToListAsync();
        }

        public void Update(Comment comment)
        {
            _dbContext.Comments.Update(comment);
        }

        public async Task<List<Comment>> GetByCommentId(Guid id)
        {
            return await _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsNoTracking()
                .Where(c => c.ParentCommentId == id)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetByCommentIdAndAuthorId(Guid commentId, Guid authorId)
        {
            return await _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsNoTracking()
                .Where(comment => comment.ParentCommentId == commentId && comment.UserId == authorId)
                .ToListAsync();
        }
    }
}

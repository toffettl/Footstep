using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Comments;
using Microsoft.EntityFrameworkCore;

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

        public async Task<(List<Comment> Comments, int TotalCount)> GetByUserId(Guid id, int page, int pageSize)
        {
            var query = _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsSplitQuery()
                .AsNoTracking()
                .Where(c => c.UserId == id);

            var totalCount = await query.CountAsync();

            var comments = await query
                .OrderByDescending(c => c.CommentLikes.Count)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (comments, totalCount);
        }

        public async Task<Comment?> GetById(Guid id)
        {
            return await _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Comment>> GetByPointOfInterestIdAndUserId(Guid pointOfInterestId, Guid authorId)
        {
            return await _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsSplitQuery()
                .AsNoTracking()
                .Where(c => c.ParentPointOfInterestId == pointOfInterestId && c.UserId == authorId)
                .ToListAsync();
        }

        public async Task<(List<Comment> Comments, int TotalCount)> GetByPointOfInterestId(Guid id, int page, int pageSize)
        {
            var query = _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsSplitQuery()
                .AsNoTracking()
                .Where(c => c.ParentPointOfInterestId == id);

            var totalCount = await query.CountAsync();

            var comments = await query
                .OrderByDescending(c => c.CommentLikes.Count)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (comments, totalCount);
        }

        public void Update(Comment comment)
        {
            _dbContext.Comments.Update(comment);
        }

        public async Task<(List<Comment> Comments, int TotalCount)> GetByCommentId(Guid id, int page, int pageSize)
        {
            var query = _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsSplitQuery()
                .AsNoTracking()
                .Where(c => c.ParentCommentId == id);

            var totalCount = await query.CountAsync();

            var comments = await query
                .OrderByDescending(c => c.CommentLikes.Count)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (comments, totalCount);
        }

        public async Task<List<Comment>> GetByCommentIdAndUserId(Guid commentId, Guid authorId)
        {
            return await _dbContext.Comments
                .Include(c => c.CommentLikes)
                .Include(c => c.Comments)
                .AsSplitQuery()
                .AsNoTracking()
                .Where(comment => comment.ParentCommentId == commentId && comment.UserId == authorId)
                .ToListAsync();
        }
    }
}

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

        public async Task<(List<Comment> Comments, int TotalCount)> GetByAuthorId(Guid id, int page, int pageSize)
        {
            var query = _dbContext.Comments
                .AsNoTracking()
                .Where(comment => comment.AuthorId == id);

            var totalCount = await query.CountAsync();

            var comments = await query
                .OrderByDescending(c => c.Likes)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (comments, totalCount);
        }

        public async Task<Comment> GetById(Guid id)
        {
            return await _dbContext.Comments.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Comment>> GetByParentIdAndAuthorId(Guid parentId, Guid authorId)
        {
            return await _dbContext.Comments.AsNoTracking()
                .Where(comment => comment.ParentId == parentId && comment.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<(List<Comment> Comments, int TotalCount)> GetByParentsId(Guid id, int page, int pageSize)
        {
            var query = _dbContext.Comments
                .AsNoTracking()
                .Where(comment => comment.ParentId == id);

            var totalCount = await query.CountAsync();

            var comments = await query
                .OrderByDescending(c => c.Likes)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (comments, totalCount);
        }

        public void Update(Comment comment)
        {
            _dbContext.Comments.Update(comment);
        }
    }
}

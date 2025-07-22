using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Comments;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class CommentRepository : ICommentsWriteOnlyRepository,
        ICommentsReadOnlyRepository
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

        public async Task<List<Comment>> GetByAuthorId(Guid id)
        {
            return await _dbContext.Comments.AsNoTracking().Where(comment => comment.AuthorId == id).ToListAsync();
        }

        public async Task<List<Comment>> GetByParentIdAndAuthorId(Guid parentId, Guid authorId)
        {
            return await _dbContext.Comments.AsNoTracking()
                .Where(comment => comment.ParentId == parentId && comment.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetByParentsId(Guid id)
        {
            return await _dbContext.Comments.AsNoTracking().Where(comment => comment.ParentId == id).ToListAsync();
        }
    }
}

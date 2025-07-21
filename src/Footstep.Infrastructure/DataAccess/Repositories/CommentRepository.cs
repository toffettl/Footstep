using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Comments;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class CommentRepository : ICommentsWriteOnlyRepository
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
    }
}

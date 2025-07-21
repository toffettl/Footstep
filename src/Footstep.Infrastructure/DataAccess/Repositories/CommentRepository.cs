using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Comments;

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
    }
}

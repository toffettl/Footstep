using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Marks;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class MarkRepository : IMarkWriteOnlyRepository, IMarkReadOnlyRepository, IMarkUpdateOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;

        public MarkRepository(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Mark mark)
        {
            await _dbContext.Marks.AddAsync(mark);
        }

        public async Task<List<Mark>> GetAll()
        {
            return await _dbContext.Marks.AsNoTracking().ToListAsync();
        }

        public async Task<Mark?> GetById(Guid id)
        {
            return await _dbContext.Marks.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Update(Mark mark)
        {
            _dbContext.Marks.Update(mark);
        }
    }
}

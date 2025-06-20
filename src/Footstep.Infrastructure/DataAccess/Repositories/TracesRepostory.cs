using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Traces;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class TracesRepostory : ITracesWriteOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;
        public TracesRepostory(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Trace trace)
        {
            await _dbContext.Traces.AddAsync(trace);
        }
    }
}

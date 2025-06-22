using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Traces;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class TracesRepostory : ITracesWriteOnlyRepository,
        ITracesUpdateOnlyRepository,
        ITracesReadOnlyRepository
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

       public async Task<bool?> Delete(Guid id)
        {
            var result = await _dbContext.Traces.FirstOrDefaultAsync(t => t.Id == id);
            if (result == null)
            {
                return false;
            }

            _dbContext.Traces.Remove(result);

            return true;
        }

        async Task<Trace?> ITracesUpdateOnlyRepository.GetById(Guid id)
        {
            return await _dbContext.Traces.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Update(Trace trace)
        {
            _dbContext.Traces.Update(trace);
        }

        public async Task<List<Trace>> GetAll()
        {
            return await _dbContext.Traces.AsNoTracking().ToListAsync();
        }

        async Task<Trace?> ITracesReadOnlyRepository.GetById(Guid id)
        {
            return await _dbContext.Traces.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}

using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Traces;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class PointOfInterestRepository : IPointsOfInterestWriteOnlyRepository,
        IPointsOfInterestUpdateOnlyRepository,
        IPointsOfInterestReadOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;
        public PointOfInterestRepository(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(PointOfInterest pointOfInterest)
        {
            await _dbContext.PointOfInterests.AddAsync(pointOfInterest);
        }

       public async Task<bool?> Delete(Guid id)
        {
            var result = await _dbContext.PointOfInterests.FirstOrDefaultAsync(t => t.Id == id);

            if (result == null)
            {
                return false;
            }

            _dbContext.PointOfInterests.Remove(result);

            return true;
        }

        public void Update(PointOfInterest trace)
        {
            _dbContext.PointOfInterests.Update(trace);
        }

        public async Task<List<PointOfInterest>> GetAll()
        {
            return await _dbContext.PointOfInterests.Include(p => p.Address).AsNoTracking().ToListAsync();
        }

        public async Task<PointOfInterest?> GetById(Guid id)
        {
            return await _dbContext.PointOfInterests.Include(p => p.Address).AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}

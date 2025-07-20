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
        public async Task Add(Domain.Entities.PointOfInterest pointOfInterest)
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

        async Task<Domain.Entities.PointOfInterest?> IPointsOfInterestUpdateOnlyRepository.GetById(Guid id)
        {
            return await _dbContext.PointOfInterests.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Update(Domain.Entities.PointOfInterest pointOfInterest)
        {
            _dbContext.PointOfInterests.Update(pointOfInterest);
        }

        public async Task<List<Domain.Entities.PointOfInterest>> GetAll()
        {
            return await _dbContext.PointOfInterests.AsNoTracking().ToListAsync();
        }

        async Task<Domain.Entities.PointOfInterest?> IPointsOfInterestReadOnlyRepository.GetById(Guid id)
        {
            return await _dbContext.PointOfInterests.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}

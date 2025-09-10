using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Traces;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class PointOfInterestRepository : 
        IPointOfInterestWriteOnlyRepository,
        IPointOfInterestUpdateOnlyRepository,
        IPointOfInterestReadOnlyRepository
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

        public void Update(PointOfInterest pointOfInterest)
        {
            _dbContext.PointOfInterests.Update(pointOfInterest);
        }

        public async Task<List<PointOfInterest>> GetAll()
        {
            return await _dbContext.PointOfInterests
                .AsSplitQuery()
                .Include(poi => poi.Address)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PointOfInterest?> GetById(Guid id)
        {
            return await _dbContext.PointOfInterests
                .AsSplitQuery()
                .Include(poi => poi.User)
                .Include(poi => poi.Address)
                .AsNoTracking()
                .FirstOrDefaultAsync(poi => poi.Id == id);
        }

        public async Task<(List<PointOfInterest> PointsOfInterest, int TotalCount)> GetAllByPage(int page, int pageSize)
        {
            var query = _dbContext.PointOfInterests
                .AsSplitQuery()
                .Include(poi => poi.Address)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var pointsOfInterest = await query
                .OrderBy(p => p.Comments.Count)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (pointsOfInterest, totalCount);
        }
    }
}

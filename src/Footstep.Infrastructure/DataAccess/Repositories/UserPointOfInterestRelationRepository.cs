using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.UserPointOfInterestRelations;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class UserPointOfInterestRelationRepository : 
        IUserPointOfInterestRelationReadOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;

        public UserPointOfInterestRelationRepository(
            FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserPointOfInterestRelation?> GetByUserIdAndPointOfInterestId(Guid userId, Guid pointOfInterestId)
        {
            return await _dbContext.UserPointOfInterestRelations
                .AsNoTracking()
                .FirstOrDefaultAsync(upoir => upoir.UserId == userId && upoir.PointOfInterestId == pointOfInterestId);
        }
    }
}

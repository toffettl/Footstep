using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.UserPointOfInterestRelations;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class UserPointOfInterestRelationRepository : 
        IUserPointOfInterestRelationWriteOnlyRepository,
        IUserPointOfInterestRelationReadOnlyRepository,
        IUserPointOfInterestRelationUpdateOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;

        public UserPointOfInterestRelationRepository(
            FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(UserPointOfInterestRelation userPointOfInterestRelation)
        {
            await _dbContext.UserPointOfInterestRelations.AddAsync(userPointOfInterestRelation);
        }

        public async Task<UserPointOfInterestRelation?> GetByUserIdAndPointOfInterestId(Guid pointOfInterestId, Guid userId)
        {
            return await _dbContext.UserPointOfInterestRelations
                .FirstOrDefaultAsync(upoir => upoir.UserId == userId && upoir.PointOfInterestId == pointOfInterestId);
        }

        public void Update(UserPointOfInterestRelation userPointOfInterestRelation)
        {
            _dbContext.Update(userPointOfInterestRelation);
        }
    }
}

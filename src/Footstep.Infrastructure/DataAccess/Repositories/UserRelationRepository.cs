using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.RelationUser;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories;
public class UserRelationRepository : IUserRelationWriteOnlyRepository, IUserRelationReadOnlyRepository
{
    private readonly FootstepDbContext _dbContext;
    public UserRelationRepository(FootstepDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddRelation(UserRelation userRelation)
    {
        await _dbContext.UserRelations.AddAsync(userRelation);
    }

    public async Task<bool> IsFollowingAsync(Guid followerId, Guid followingId)
    {
        return await _dbContext.UserRelations.AnyAsync(user => user.FollowerId == followerId && user.FollowingId == followingId);
    }
}

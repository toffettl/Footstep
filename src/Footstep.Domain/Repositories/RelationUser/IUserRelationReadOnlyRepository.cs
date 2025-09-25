using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.RelationUser;
public interface IUserRelationReadOnlyRepository
{
    Task<bool> IsFollowingAsync(Guid followerId, Guid followingId);
    Task<List<Followership>> GetFollowers(Guid followingId);
    Task<List<Followership>> GetFollowing(Guid followerId);
}

using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.RelationUser;
public interface IUserRelationWriteOnlyRepository
{
    Task AddRelation(Followership userRelation);
    Task<bool?> DeleteRelation(Guid followerId, Guid followingId);
}

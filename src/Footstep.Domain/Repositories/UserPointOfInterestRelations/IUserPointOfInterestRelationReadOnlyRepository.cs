using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.UserPointOfInterestRelations
{
    public interface IUserPointOfInterestRelationReadOnlyRepository
    {
        Task<UserPointOfInterestRelation?> GetByUserIdAndPointOfInterestId(Guid userId, Guid pointOfInterestId);
    }
}

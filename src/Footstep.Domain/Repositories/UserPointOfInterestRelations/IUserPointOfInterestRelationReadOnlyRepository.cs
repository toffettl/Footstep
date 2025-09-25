using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.UserPointOfInterestRelations
{
    public interface IUserPointOfInterestRelationReadOnlyRepository
    {
        Task<UserPointOfInterestRelation?> GetByUserIdAndPointOfInterestId(Guid pointOfInterestId, Guid userId);
    }
}

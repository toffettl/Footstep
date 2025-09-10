using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.UserPointOfInterestRelations
{
    public interface IUserPointOfInterestRelationWriteOnlyRepository
    {
        Task Add(UserPointOfInterestRelation userPointOfInterestRelation);
    }
}

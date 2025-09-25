using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.UserPointOfInterestRelations
{
    public interface IUserPointOfInterestRelationUpdateOnlyRepository
    {
        void Update(UserPointOfInterestRelation userPointOfInterestRelation);
    }
}

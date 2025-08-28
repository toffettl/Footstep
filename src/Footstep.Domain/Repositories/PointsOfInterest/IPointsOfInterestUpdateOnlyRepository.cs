using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Traces
{
    public interface IPointsOfInterestUpdateOnlyRepository
    {
        void Update(PointOfInterest pointOfInterest);
    }
}

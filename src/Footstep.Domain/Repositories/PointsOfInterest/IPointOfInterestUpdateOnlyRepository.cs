using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Traces
{
    public interface IPointOfInterestUpdateOnlyRepository
    {
        void Update(PointOfInterest pointOfInterest);
    }
}

using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Traces
{
    public interface IPointsOfInterestUpdateOnlyRepository
    {
        Task<PointOfInterest> GetById(Guid id);
        void Update(PointOfInterest pointOfInterest);
    }
}

using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Traces
{
    public interface IPointsOfInterestWriteOnlyRepository
    {
        Task Add(PointOfInterest pointOfInterest);
        Task<bool?> Delete(Guid id);
    }
}

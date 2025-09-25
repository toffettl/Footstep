using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Traces
{
    public interface IPointOfInterestWriteOnlyRepository
    {
        Task Add(PointOfInterest pointOfInterest);
        Task<bool?> Delete(Guid id);
    }
}

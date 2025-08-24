using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Traces
{
    public interface IPointsOfInterestReadOnlyRepository
    {
        Task<List<PointOfInterest>> GetAll();
        Task<PointOfInterest?> GetById(Guid id);
    }
}


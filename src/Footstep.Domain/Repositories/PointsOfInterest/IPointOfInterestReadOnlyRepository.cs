using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Traces
{
    public interface IPointOfInterestReadOnlyRepository
    {
        Task<List<PointOfInterest>> GetAll();
        Task<(List<PointOfInterest> PointsOfInterest, int TotalCount)> GetAllByPage(int page, int pageSize);
        Task<PointOfInterest?> GetById(Guid id);
    }
}


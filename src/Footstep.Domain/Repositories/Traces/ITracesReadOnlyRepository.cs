using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Traces
{
    public interface ITracesReadOnlyRepository
    {
        Task<List<Trace>> GetAll();
        Task<Trace?> GetById(Guid id);
    }
}

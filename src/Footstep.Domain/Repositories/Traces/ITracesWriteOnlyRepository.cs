using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Traces
{
    public interface ITracesWriteOnlyRepository
    {
        Task Add(Trace trace);
        Task<bool?> Delete(Guid id);
    }
}

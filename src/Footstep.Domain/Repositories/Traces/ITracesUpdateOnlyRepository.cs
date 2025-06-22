using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Traces
{
    public interface ITracesUpdateOnlyRepository
    {
        Task<Trace?> GetById(Guid id);
        void Update(Trace trace);
    }
}

using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Marks
{
    public interface IMarkReadOnlyRepository
    {
        Task<Mark?> GetById(Guid id);
    }
}

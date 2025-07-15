using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Marks
{
    public interface IMarkWriteOnlyRepository
    {
        Task Add(Mark mark);
        Task<bool?> Delete(Guid id);
    }
}

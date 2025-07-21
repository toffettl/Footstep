using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Comments
{
    public interface ICommentsWriteOnlyRepository
    {
        Task Add(Comment comment);
        Task<bool?> Delete(Guid id);
    }
}

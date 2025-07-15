using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Marks
{
    public interface IMarkReadOnlyRepository
    {
        Task<List<Mark>> GetAll();
        Task<Mark?> GetById(Guid id);
    }
}

using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Marks
{
    public interface IMarkUpdateOnlyRepository
    {
        Task<Mark?> GetById(Guid id);
        void Update(Mark mark);
    }
}

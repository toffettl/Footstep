using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Styles
{
    public interface IStyleReadOnlyRepository
    {
        Task<Style?> GetByName(string name);
    }
}

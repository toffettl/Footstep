using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Items
{
    public interface IItemReadOnlyRepository
    {
        Task<Item?> GetByPreferenceIdAndStyleId(Guid preferenceId, Guid styleId);
    }
}

using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Items
{
    public interface IItemReadOnlyRepository
    {
        Task<Item?> GetByPreferenceIdAndStyleId(Guid preferenceId, Guid styleId);
        Task<List<Item>> GetByPreferenceId(Guid preferenceId);
        Task<List<Item>> GetByPreferenceIdAndUnlocked(Guid preferenceId);
    }
}

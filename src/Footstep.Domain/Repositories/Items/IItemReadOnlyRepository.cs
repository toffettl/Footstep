using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Items
{
    public interface IItemReadOnlyRepository
    {
        Task<Item?> GetByPreferenceIdAndStyleId(Guid preferenceId, Guid styleId);
        Task<List<Item>> GetByPreferenceId(Guid preferenceId);
        Task<List<Item>> GetByPreferenceIdAndUnlocked(Guid preferenceId);
        Task<Item?> GetById(Guid id);
        Task<List<Item>> GetAllShopItems();
        Task<List<Item>> GetAvailableForUser(Guid userId);
        Task<List<Item>> GetUserPurchasedItems(Guid userId);
       
    }
}

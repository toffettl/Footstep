using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Items;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class ItemRepository : IItemWriteOnlyRepository, IItemReadOnlyRepository, IItemUpdateOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;

        public ItemRepository(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Item item)
        {
            await _dbContext.Items.AddAsync(item);
        }

        public async Task<List<Item>> GetByPreferenceId(Guid preferenceId)
        {
            return await _dbContext.Items
                .Include(i => i.Style)
                .AsSplitQuery()
                .AsNoTracking()
                .Where(i => i.PreferenceId == preferenceId)
                .ToListAsync();
        }

        public async Task<Item?> GetByPreferenceIdAndStyleId(Guid preferenceId, Guid styleId)
        {
            return await _dbContext.Items.AsNoTracking().FirstOrDefaultAsync(i => i.PreferenceId == preferenceId && i.StyleId == styleId);
        }

        public async Task<List<Item>> GetByPreferenceIdAndUnlocked(Guid preferenceId)
        {
            return await _dbContext.Items
                .Include(i => i.Style)
                .AsSplitQuery()
                .AsNoTracking()
                .Where(i => i.PreferenceId == preferenceId && i.Unlocked)
                .ToListAsync();
        }

        public void Update(Item item)
        {
            _dbContext.Items.Update(item);
        }

        public async Task<Item?> GetById(Guid id)
        {
            return await _dbContext.Items
                 .AsNoTracking()
                 .Include(i => i.Style)
                 .Include(i => i.Preference)
                 .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Item>> GetAllShopItems()
        {
            return await _dbContext.Items
                 .AsNoTracking()
                 .Include(i => i.Style)
                 .Include(i => i.Preference)
                 .Where(i => i.IsAvaliableInShop)
                 .ToListAsync();
        }

        public async Task<List<Item>> GetAvailableForUser(Guid userId)
        {
            var purchasedItemIds = await _dbContext.UserItems
                .AsNoTracking()
                .Where(ui => ui.UserId == userId)
                .Select(ui => ui.ItemId)
                .ToListAsync();

            return await _dbContext.Items
                .AsNoTracking()
                .Include(i => i.Style)
                .Where(i => i.IsAvaliableInShop && !purchasedItemIds.Contains(i.Id))
                .OrderBy(i => i.Price)
                .ToListAsync();
        }

        public async Task<List<Item>> GetUserPurchasedItems(Guid userId)
        {
            return await _dbContext.UserItems
                .AsNoTracking()
                .Where(ui => ui.UserId == userId)
                .Include(ui => ui.Item)
                    .ThenInclude(i => i.Style)
                .Include(ui => ui.Item)
                    .ThenInclude(i => i.Preference)
                .Select(ui => ui.Item!)
                .ToListAsync();
        }
    }
}

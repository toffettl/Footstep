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
    }
}

using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Items;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class ItemRepository : IItemWriteOnlyRepository, IItemReadOnlyRepository
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

        public async Task<Item?> GetByPreferenceIdAndStyleId(Guid preferenceId, Guid styleId)
        {
            return await _dbContext.Items.AsNoTracking().FirstOrDefaultAsync(item => item.PreferenceId == preferenceId && item.StyleId == styleId);
        }
    }
}

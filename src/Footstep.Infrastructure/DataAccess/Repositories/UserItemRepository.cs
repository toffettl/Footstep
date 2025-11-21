using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Domain.Repositories.UserItem;
using Footstep.Domain.Repositories.UserItems;
using Footstep.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public  class UserItemRepository : IUserItemReadOnlyRepository, IUserItemWriteOnlyRepository
    {
        private readonly FootstepDbContext _context;
        public UserItemRepository(FootstepDbContext context)
        {
            _context = context;
        }

        public async Task Add(Domain.Entities.UserItem userItem)
        {
            await _context.UserItems.AddAsync(userItem);
        }

        public async Task<bool> HasUserPurchasedAsync(Guid userId, Guid itemId)
        {
            return await _context.UserItems
                .AsNoTracking()
                .AnyAsync(ui => ui.UserId == userId && ui.ItemId == itemId);
        }

        public async Task<List<Domain.Entities.UserItem>> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserItems
                .AsNoTracking()
                .Where(ui => ui.UserId == userId)
                .Include(ui => ui.Item)
                .ToListAsync();
        }
    }
}

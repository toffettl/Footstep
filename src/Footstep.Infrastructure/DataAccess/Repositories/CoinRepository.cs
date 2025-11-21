using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Domain.Repositories.Coins;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class CoinRepository : ICoinReadOnlyRepository, ICoinUpdateOnlyRepository
    {
        private readonly FootstepDbContext _context;
        public CoinRepository(FootstepDbContext context)
        {
            _context = context;
        }
        public async Task<Domain.Entities.Coin?> GetByUserId(Guid userId)
        {
            return await _context.Coins
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public void Update(Domain.Entities.Coin coin)
        {
            _context.Coins.Update(coin);
        }
    }
}

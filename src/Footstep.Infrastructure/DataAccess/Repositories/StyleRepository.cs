using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Styles;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class StyleRepository : IStyleWriteOnlyRepository, IStyleReadOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;

        public StyleRepository(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Style style)
        {
            await _dbContext.Styles.AddAsync(style);
        }

        public async Task<bool> ExistActiveStyleWithName(string name)
        {
            return await _dbContext.Styles.AnyAsync(style => style.Name!.Equals(name));
        }
    }
}

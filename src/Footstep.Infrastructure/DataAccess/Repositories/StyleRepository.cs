using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Styles;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class StyleRepository : IStyleWriteOnlyRepository
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
    }
}

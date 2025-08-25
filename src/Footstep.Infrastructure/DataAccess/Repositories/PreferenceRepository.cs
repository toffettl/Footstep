using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Preferences;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class PreferenceRepository : IPreferenceWriteOnlyRepository, IPreferenceReadOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;

        public PreferenceRepository(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Preference preference)
        {
            await _dbContext.Preferences.AddAsync(preference);
        }

        public async Task<List<Preference>> GetAll()
        {
            return await _dbContext.Preferences.AsNoTracking().ToListAsync();
        }
    }
}

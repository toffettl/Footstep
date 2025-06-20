using Footstep.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FootstepDbContext _dbContext;
        public UnitOfWork(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using Footstep.Domain.Entities;

namespace Footstep.Infrastructure.DataAccess
{
    public class FootstepDbContext : DbContext
    {
        public FootstepDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Trace> Traces { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

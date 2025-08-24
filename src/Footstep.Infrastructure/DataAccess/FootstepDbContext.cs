using Microsoft.EntityFrameworkCore;
using Footstep.Domain.Entities;

namespace Footstep.Infrastructure.DataAccess
{
    public class FootstepDbContext : DbContext
    {
        public FootstepDbContext(DbContextOptions options) : base(options) { }

        public DbSet<PointOfInterest> PointOfInterests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRelation> UserRelations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Coin)
                .WithOne(c => c.User)
                .HasForeignKey<Coin>(c => c.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Preference)
                .WithOne(p => p.User)
                .HasForeignKey<Preference>(p => p.UserId);

            modelBuilder.Entity<UserRelation>()
                .HasOne(ur => ur.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(ur => ur.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRelation>()
                .HasOne(ur => ur.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(ur => ur.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

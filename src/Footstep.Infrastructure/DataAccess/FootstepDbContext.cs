using Microsoft.EntityFrameworkCore;
using Footstep.Domain.Entities;

namespace Footstep.Infrastructure.DataAccess
{
    public class FootstepDbContext : DbContext
    {
        public FootstepDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Trace> Traces { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.CommentsWritten)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.TargetUser)
                .WithMany(u => u.CommentsReceived)
                .HasForeignKey(c => c.TargetUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Achievement>()
                .HasOne(a => a.User)
                .WithMany(u => u.Achievements)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

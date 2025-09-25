using Microsoft.EntityFrameworkCore;
using Footstep.Domain.Entities;

namespace Footstep.Infrastructure.DataAccess
{
    public class FootstepDbContext : DbContext
    {
        public FootstepDbContext(DbContextOptions options) : base(options) { }

        public DbSet<PointOfInterest> PointOfInterests { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRelation> UserRelations { get; set; }
        public DbSet<UserPointOfInterestRelation> UserPointOfInterestRelations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Preference> Preferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Coin)
                .WithOne(c => c.User)
                .HasForeignKey<Coin>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

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

            modelBuilder.Entity<CommentLike>()
                .HasOne(cl => cl.Comment)
                .WithMany(c => c.CommentLikes)
                .HasForeignKey(cl => cl.CommentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

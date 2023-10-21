using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PicturesTask.Infrastructure.Entities;
using PicturesTask.Infrastructure.Entities.Cofiguration;
using PicturesTask.Infrastructure.Entities.MappingConfigurations;

namespace PicturesTask.Infrastructure
{
    public class UsersContext : IdentityDbContext<User>
    {
        public DbSet<Image> Images { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Invation> Invations { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany(u => u.Users);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new InviteConfiguration());
            //builder.ApplyConfiguration(new FriendsConfiguration());
            
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PicturesTask.Infrastructure.Entities;
using PicturesTask.Infrastructure.Entities.Cofiguration;

namespace PicturesTask.Infrastructure
{
    public class UsersContext : IdentityDbContext<User>
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}

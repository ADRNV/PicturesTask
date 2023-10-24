using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PicturesTask.Infrastructure.Entities.Cofiguration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(u => u.Images)
                .WithOne(u => u.User);

            builder.HasMany(u => u.Invations)
                .WithOne(u => u.From);

            builder.HasData(
                new EntityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "User",
                    NormalizedUserName = "USER",
                    PasswordHash = "AQAAAAIAAYagAAAAEJnbSpz4UNKDnq+KMzOiivLwPovhKV3SwHz8w95dQwvQWKbmo5yZa4NKTSpf8U6Muw=="//string
                },
                new EntityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "User2",
                    NormalizedUserName = "USER2",
                    PasswordHash = "AQAAAAIAAYagAAAAEJnbSpz4UNKDnq+KMzOiivLwPovhKV3SwHz8w95dQwvQWKbmo5yZa4NKTSpf8U6Muw=="//string
                });
        }
    }
}

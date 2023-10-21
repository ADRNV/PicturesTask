using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PicturesTask.Infrastructure.Entities.Cofiguration
{
    internal class FriendsConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<EntityFriend> builder)
        {

        }
    }
}

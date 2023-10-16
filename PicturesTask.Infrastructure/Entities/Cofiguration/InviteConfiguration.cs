using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PicturesTask.Infrastructure.Entities.Cofiguration
{
    public class InviteConfiguration : IEntityTypeConfiguration<EntityInvation>
    {
        public void Configure(EntityTypeBuilder<EntityInvation> builder)
        {
            builder.HasKey(i => i.Id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.Media;

namespace Sofarashel.Infra.Data.Configuration.MediaConfig
{
    public class ImageConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(i => i.ImageUrl).IsRequired().HasMaxLength(500);
        }
    }
}
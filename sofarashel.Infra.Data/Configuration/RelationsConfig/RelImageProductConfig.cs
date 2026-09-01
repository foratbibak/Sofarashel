using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.Relations;

namespace Sofarashel.Infra.Data.Configuration.RelationsConfig
{
    public class RelImageProductConfig : IEntityTypeConfiguration<Rel_Image_Product>
    {
        public void Configure(EntityTypeBuilder<Rel_Image_Product> builder)
        {
            builder.ToTable("Rel_Image_Product");

            builder.HasKey(r => new { r.ImageId, r.ProductId });

            builder.HasOne(r => r.Image)
                   .WithMany(i => i.ProductAttribute)
                   .HasForeignKey(r => r.ImageId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Product)
                   .WithMany(p => p.ProductImages)
                   .HasForeignKey(r => r.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
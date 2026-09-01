using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.Relations;

namespace Sofarashel.Infra.Data.Configuration.RelationsConfig
{
    public class RelAttributesFeturesProductConfig : IEntityTypeConfiguration<Rel_AttributesFetures_Product>
    {
        public void Configure(EntityTypeBuilder<Rel_AttributesFetures_Product> builder)
        {
            builder.ToTable("Rel_AttributesFetures_Product");

            builder.HasKey(a => new { a.AttributeFeatureId, a.ProductId });

            builder.HasOne(a => a.AttributeFeature)
                   .WithMany(f => f.ProductAttributes)
                   .HasForeignKey(a => a.AttributeFeatureId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Product)
                   .WithMany(p => p.ProductAttributes)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
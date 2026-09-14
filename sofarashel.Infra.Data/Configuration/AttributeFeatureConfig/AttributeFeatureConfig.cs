using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.AttributeFeatures;
using Sofarashel.Domain.Models.Products;

namespace Sofarashel.Infra.Data.Configuration.AttributeFeatureConfig
{
    public class AttributeFeatureConfig : IEntityTypeConfiguration<AttributeFeature>
    {
        public void Configure(EntityTypeBuilder<AttributeFeature> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(a => a.AttributeTitle).IsRequired().HasMaxLength(200);
            builder.Property(a => a.AttributeValue).IsRequired().HasMaxLength(500);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.Categories;

namespace Sofarashel.Infra.Data.Configuration.CategoryConfig
{
    public class ProductDetailConfig : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.HasKey(pd => pd.Id);
            builder.Property(pd => pd.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(pd => pd.Material).HasMaxLength(200);
            builder.Property(pd => pd.FabricType).HasMaxLength(200);
            builder.Property(pd => pd.Color).HasMaxLength(100);
            builder.Property(pd => pd.Style).HasMaxLength(200);
            builder.Property(pd => pd.Length).HasColumnType("decimal(6,2)");
            builder.Property(pd => pd.Width).HasColumnType("decimal(6,2)");
            builder.Property(pd => pd.Height).HasColumnType("decimal(6,2)");

            builder.HasOne(pd => pd.Category)
                   .WithOne(c => c.ProductDetail)
                   .HasForeignKey<ProductDetail>(pd => pd.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(pd => pd.CategoryId).IsUnique();
        }
    }
}
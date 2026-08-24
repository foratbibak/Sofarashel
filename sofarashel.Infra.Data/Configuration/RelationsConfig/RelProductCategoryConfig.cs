using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.Relations;

namespace Sofarashel.Infra.Data.Configuration.RelationsConfig
{
    public class RelProductCategoryConfig : IEntityTypeConfiguration<Rel_Product_Category>
    {
        public void Configure(EntityTypeBuilder<Rel_Product_Category> builder)
        {
            builder.ToTable("Rel_Product_Category");

            builder.HasKey(rpc => new { rpc.ProductId, rpc.CategoryId });

            builder.HasOne(rpc => rpc.Product)
                   .WithMany(p => p.ProductCategories)
                   .HasForeignKey(rpc => rpc.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rpc => rpc.Category)
                   .WithMany(c => c.ProductCategories)
                   .HasForeignKey(rpc => rpc.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
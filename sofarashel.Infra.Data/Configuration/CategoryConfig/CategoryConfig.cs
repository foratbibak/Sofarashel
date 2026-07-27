using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Infra.Data.Configuration.CategoryConfig
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            #region Key
            builder.HasKey(c => c.Id);
            #endregion

            #region Validations
            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();

            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);

            builder.Property(c => c.Description).HasMaxLength(1000);

            builder.Property(c => c.Material).HasMaxLength(100);

            builder.Property(c => c.FabricType).HasMaxLength(100);

            builder.Property(c => c.Color).HasMaxLength(50);

            builder.Property(c => c.Style).HasMaxLength(100);

            builder.Property(c => c.Length).HasColumnType("decimal(6,2)");

            builder.Property(c => c.Width).HasColumnType("decimal(6,2)");

            builder.Property(c => c.Height).HasColumnType("decimal(6,2)");
            #endregion

            #region Realtions
            builder.HasOne(c => c.Parent)
                   .WithMany(c => c.Children)
                   .HasForeignKey(c => c.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}

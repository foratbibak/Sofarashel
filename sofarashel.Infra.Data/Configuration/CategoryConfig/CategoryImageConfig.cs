using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Infra.Data.Configuration.CategoryConfig
{
    public class CategoryImageConfig : IEntityTypeConfiguration<CategoryImage>
    {
        public void Configure(EntityTypeBuilder<CategoryImage> builder)
        {
            #region Key
            builder.HasKey(ci => ci.Id);
            #endregion

            #region Validations
            builder.Property(ci => ci.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();

            builder.Property(ci => ci.ImageUrl).IsRequired().HasMaxLength(500);
            #endregion

            #region Realtions
            builder.HasOne(ci => ci.Category)
                   .WithMany(c => c.Images)
                   .HasForeignKey(ci => ci.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}

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
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Description).HasMaxLength(1000);

            builder.HasOne(c => c.Parent)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
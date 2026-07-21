using Sofarashel.Domain.Models.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Configuration.RoleConfig
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            #region Key
            builder.HasKey(r => r.Id);
            #endregion
            #region Validations
            builder.Property(r => r.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();
            builder.Property(r => r.RoleName).IsRequired().HasMaxLength(200);
            #endregion

            #region Realtions
            builder.HasMany(r => r.UserInRole).WithOne(r => r.Role).HasForeignKey(r => r.RoleId);
            #endregion
        }
    }
}

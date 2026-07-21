using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Ifra.Data.Configuration.PermissionConfig
{
    public class RolePermissionMappingConfig : IEntityTypeConfiguration<RolePermissionMapping>
    {
        public void Configure(EntityTypeBuilder<RolePermissionMapping> builder)
        {
            builder.HasKey(x => new
            {
                x.PermissionId,
                x.RoleId,
            });
        }
    }
}

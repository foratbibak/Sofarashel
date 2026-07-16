using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using User_Login.Models.User;

namespace Bibaket.Ifra.Data.Configuration.UserConfig
{
    public class UserConfig
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Key
            builder.HasKey(u => u.Id);
            #endregion
            #region Validations
            builder.Property(u => u.FirstName).HasMaxLength(200);
            builder.Property(u => u.LastName).HasMaxLength(200);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(200);

            #endregion

            #region Realtions
            #endregion
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofarashel.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Ifra.Data.Configuration.PermissionConfig
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UniqName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.DisplayName).HasMaxLength(200).IsRequired();

            builder.HasMany(x => x.RolePermissionMappings).WithOne(x => x.Permission).HasForeignKey(x => x.PermissionId);

            builder.HasOne(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId);

            #region Seed All Permissions
            var seedDate = new DateTime(2026, 1, 1);

            builder.HasData(
                new Permission { Id = 1, UniqName = "ManageUsers", DisplayName = "مدیریت کاربران", ParentId = null, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 2, UniqName = "AddUser", DisplayName = "افزودن کاربر", ParentId = 1, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 3, UniqName = "EditUser", DisplayName = "ویرایش کاربر", ParentId = 1, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 4, UniqName = "DeleteUser", DisplayName = "حذف کاربر", ParentId = 1, CreatDate = seedDate, IsDelete = false },

                new Permission { Id = 5, UniqName = "ManageCategories", DisplayName = "مدیریت دسته‌بندی‌ها", ParentId = null, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 6, UniqName = "AddCategory", DisplayName = "افزودن دسته‌بندی", ParentId = 5, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 7, UniqName = "EditCategory", DisplayName = "ویرایش دسته‌بندی", ParentId = 5, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 8, UniqName = "DeleteCategory", DisplayName = "حذف دسته‌بندی", ParentId = 5, CreatDate = seedDate, IsDelete = false },

                new Permission { Id = 9, UniqName = "ManageRoles", DisplayName = "مدیریت نقش‌ها", ParentId = null, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 10, UniqName = "AddRole", DisplayName = "افزودن نقش", ParentId = 9, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 11, UniqName = "EditRole", DisplayName = "ویرایش نقش", ParentId = 9, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 12, UniqName = "DeleteRole", DisplayName = "حذف نقش", ParentId = 9, CreatDate = seedDate, IsDelete = false },

                new Permission { Id = 13, UniqName = "ManageProducts", DisplayName = "مدیریت محصولات", ParentId = null, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 14, UniqName = "AddProduct", DisplayName = "افزودن محصول", ParentId = 13, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 15, UniqName = "EditProduct", DisplayName = "ویرایش محصول", ParentId = 13, CreatDate = seedDate, IsDelete = false },
                new Permission { Id = 16, UniqName = "DeleteProduct", DisplayName = "حذف محصول", ParentId = 13, CreatDate = seedDate, IsDelete = false }
            );
            #endregion
        }
    }
}
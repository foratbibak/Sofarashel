using Microsoft.EntityFrameworkCore;
using Sofarashel.Domain.Models.Categories;
using Sofarashel.Domain.Models.Permission;
using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.Models.Relations;
using Sofarashel.Domain.Models.Roles;
using Sofarashel.Models.User;
using System.Data;

namespace Sofarashel.Data
{
    public class GallaryDbcontext(DbContextOptions<GallaryDbcontext> options):
        DbContext(options)
    {
        #region Users
        public DbSet<User> Users { get; set; }
        #endregion

        #region Role
        public DbSet<Role> Role { get; set; }
        public DbSet<UserInRoles> UserInRoles { get; set; }
        #endregion

        #region Permissions
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }

        #endregion

        #region Categories
        public DbSet<Category> Categories { get; set; }
        #endregion

        #region Products
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        #endregion

        #region Relations
        public DbSet<Rel_Product_Category> Rel_Product_Categories { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Query Fillter
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(pi => !pi.IsDelete);
            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

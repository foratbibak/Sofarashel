using Microsoft.EntityFrameworkCore;
using Sofarashel.Domain.Models.Categories;
using Sofarashel.Domain.Models.Media;
using Sofarashel.Domain.Models.Permission;
using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.Models.Relations;
using Sofarashel.Domain.Models.Roles;
using Sofarashel.Models.User;
using System.Data;

namespace Sofarashel.Data
{
    public class GallaryDbcontext(DbContextOptions<GallaryDbcontext> options) :
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
        public DbSet<AttributeFeature> AttributeFeatures { get; set; }
        #endregion

        #region Media
        public DbSet<Image> Images { get; set; }
        #endregion

        #region Relations
        public DbSet<Rel_Product_Category> Rel_Product_Category { get; set; }
        public DbSet<Rel_Image_Product> Rel_Image_Product { get; set; }
        public DbSet<Rel_AttributesFetures_Product> Rel_AttributesFetures_Product { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Query Fillter
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Image>().HasQueryFilter(i => !i.IsDelete);
            modelBuilder.Entity<AttributeFeature>().HasQueryFilter(a => !a.IsDelete);
            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
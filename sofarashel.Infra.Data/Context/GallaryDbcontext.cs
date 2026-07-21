using Microsoft.EntityFrameworkCore;
using Sofarashel.Domain.Models.Permission;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Query Fillter
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDelete);
            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

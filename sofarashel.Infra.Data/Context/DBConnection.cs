using Microsoft.EntityFrameworkCore;
using System.Data;
using User_Login.Models.User;

namespace Sofarashel.Data
{
    public class DBConnection(DbContextOptions<DBConnection> options):
        DbContext(options)
    {
        #region Users
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Query Fillter
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            #endregion
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

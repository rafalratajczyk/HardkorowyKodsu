using HardkorowyKodsu.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace HardkorowyKodsu.Server.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<TableInfo> Tables { get; set; }
        public DbSet<ColumnInfo> Columns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColumnInfo>()
                       .HasNoKey()
                       .ToView(null);

            modelBuilder.Entity<TableInfo>()
                       .HasNoKey()
                       .ToView(null);
        }
    }
}

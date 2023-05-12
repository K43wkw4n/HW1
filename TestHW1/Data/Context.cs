using Microsoft.EntityFrameworkCore;
using TestHW1.Models;

namespace TestHW1.Data
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-DTGB06O; Database=TestHomeWork3; Trusted_connection=true; TrustServerCertificate=true");
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Roles)
        //        .WithMany(y => y.Users)
        //        .UsingEntity(j => j.ToTable("UserRoles"));
        //}

    }
}

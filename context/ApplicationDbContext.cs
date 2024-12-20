using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RestoranBoshqaruvi.Models;

namespace RestoranBoshqaruvi.context
{
    // Models/ApplicationDbContext.cs
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Users> Users { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
     .HasKey(u => u.UserId); // Agar UserId ishlatilgan bo'lsa
        }

      
    }

}

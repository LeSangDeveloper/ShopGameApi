using ShopGameApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ShopGameApi.Data
{
    public class ShopGameApiDBContext : DbContext
    {
        public ShopGameApiDBContext(DbContextOptions<ShopGameApiDBContext> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGame>().HasKey(ug => new { ug.UserId, ug.GameId });
            modelBuilder.Entity<UserGame>().HasOne(ug => ug.Game).WithMany(g => g.UserGame).HasForeignKey(ug => ug.GameId);
            modelBuilder.Entity<UserGame>().HasOne(ug => ug.User).WithMany(u => u.UserGame).HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<CategoryGame>().HasKey(cg => new { cg.CategoryId, cg.GameId });
            modelBuilder.Entity<CategoryGame>().HasOne(cg => cg.Category).WithMany(c => c.CategoryGame).HasForeignKey(cg => cg.CategoryId);
            modelBuilder.Entity<CategoryGame>().HasOne(cg => cg.Game).WithMany(g => g.CategoryGame).HasForeignKey(cg => cg.GameId);

            modelBuilder.Entity<Company>().HasMany(c => c.Games).WithOne(g => g.Company).IsRequired();

            modelBuilder.Entity<Game>().HasOne(g => g.Rating).WithOne(r => r.Game).HasForeignKey<Rating>(e => e.GameRef);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public DbSet<UserGame> UserGame { get; set; }
        public DbSet<CategoryGame> CategoryGame { get; set; }

    }
}
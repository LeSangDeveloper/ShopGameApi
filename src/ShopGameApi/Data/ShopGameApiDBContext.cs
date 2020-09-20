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
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
        public DbSet<Company> Companies { get; set; }

    }
}
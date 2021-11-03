using Microsoft.EntityFrameworkCore;
using Plesnik.Eshop.Web.Models.Entity;

namespace Plesnik.Eshop.Web.Models.Database
{
    public class EShopDbContext : DbContext
    {
        public DbSet<CarouselItem> CarouselItems { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }

        public EShopDbContext(DbContextOptions options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarouselItem>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<ProductItem>()
                .HasKey(e => e.Id);
        }
    }
}

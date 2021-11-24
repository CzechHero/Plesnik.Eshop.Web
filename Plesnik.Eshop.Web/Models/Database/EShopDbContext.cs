using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Plesnik.Eshop.Web.Models.Entity;
using Plesnik.Eshop.Web.Models.Identity;

namespace Plesnik.Eshop.Web.Models.Database
{
    public class EShopDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<CarouselItem> CarouselItems { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }

        public EShopDbContext(DbContextOptions options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var entity in entityTypes)
            {
                entity.SetTableName(entity.GetTableName().Replace("AspNet", ""));
            }


            //modelBuilder.Entity<CarouselItem>()
            //    .HasKey(e => e.Id);

            //modelBuilder.Entity<ProductItem>()
            //    .HasKey(e => e.Id);
        }
    }
}

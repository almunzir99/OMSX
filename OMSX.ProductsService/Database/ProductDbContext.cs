using Microsoft.EntityFrameworkCore;
using OMSX.Shared.Entities.Common;

namespace OMSX.ProductsService.Database
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
        }
        public DbSet<Entities.Product> Products { get; set; } = null!;
        public DbSet<Entities.ProductImage> ProductImages { get; set; } = null!;
        public DbSet<Entities.ProductOptions> ProductOptions { get; set; } = null!;
        public DbSet<Entities.Image> Images { get; set; } = null!;
        public DbSet<Entities.ProductAttribute> ProductAttributes { get; set; } = null!;
        public DbSet<Entities.Category> Categories { get; set; } = null!;
        public DbSet<Localization> Localizations { get; set; } = null!;

    }
}

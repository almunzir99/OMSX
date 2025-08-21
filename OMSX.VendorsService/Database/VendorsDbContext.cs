using Microsoft.EntityFrameworkCore;

namespace OMSX.VendorsService.Database
{
    public class VendorsDbContext : DbContext
    {
        public VendorsDbContext(DbContextOptions<VendorsDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendorsDbContext).Assembly);
        }
        public DbSet<Entities.Vendor> Vendors { get; set; } = null!;
        public DbSet<Entities.VendorCategory> VendorCategories { get; set; } = null!;
        public DbSet<Entities.FileInfo> FileInfos { get; set; } = null!;
        public DbSet<Shared.Entities.Common.Localization> Localizations { get; set; } = null!;
        public DbSet<Entities.BusinessAddress> BusinessAddresses { get; set; } = null!;
    }
}

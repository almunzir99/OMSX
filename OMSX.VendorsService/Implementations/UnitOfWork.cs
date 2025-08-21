using OMSX.Shared.Interfaces;
using OMSX.VendorsService.Database;
using OMSX.VendorsService.Entities;
using OMSX.VendorsService.Interfaces;

namespace OMSX.VendorsService.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VendorsDbContext _vendorsDbContext;
        public IRepositoryBase<Vendor> VendorsRepository { get; }

        public IRepositoryBase<VendorCategory> VendorCategoriesRepository { get; }

        public UnitOfWork(VendorsDbContext vendorsDbContext)
        {
            _vendorsDbContext = vendorsDbContext;
            VendorsRepository = new RepositoryBase<Vendor>(vendorsDbContext);
            VendorCategoriesRepository = new RepositoryBase<VendorCategory>(vendorsDbContext);
        }
        public Task Complete(CancellationToken cancellationToken = default)
        {
            return _vendorsDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

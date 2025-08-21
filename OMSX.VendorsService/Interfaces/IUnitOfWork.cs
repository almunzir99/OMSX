using OMSX.Shared.Interfaces;

namespace OMSX.VendorsService.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryBase<Entities.Vendor> VendorsRepository { get; }
        IRepositoryBase<Entities.VendorCategory> VendorCategoriesRepository { get; }
        Task Complete(CancellationToken cancellationToken = default);
    }
}

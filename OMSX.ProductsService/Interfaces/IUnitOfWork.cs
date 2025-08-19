using OMSX.ProductsService.Entities;
using OMSX.Shared.Interfaces;

namespace OMSX.ProductsService.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryBase<Category> CategoriesRepository { get; }
        IRepositoryBase<ProductOptions> ProductOptionsRepository { get; }
        IRepositoryBase<Product> ProductsRepository { get; }
        Task Complete(CancellationToken cancellationToken = default);
    }
}

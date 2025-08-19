using Microsoft.EntityFrameworkCore;
using OMSX.ProductsService.Database;
using OMSX.ProductsService.Entities;
using OMSX.ProductsService.Interfaces;
using OMSX.Shared.Interfaces;

namespace OMSX.ProductsService.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductDbContext _dbContext;
        public IRepositoryBase<Product> ProductsRepository { get; }
        public IRepositoryBase<Category> CategoriesRepository { get; }
        public IRepositoryBase<ProductOptions> ProductOptionsRepository { get; }
        public UnitOfWork(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
            ProductsRepository = new RepositoryBase<Product>(_dbContext);
            CategoriesRepository = new RepositoryBase<Category>(_dbContext);
            ProductOptionsRepository = new RepositoryBase<ProductOptions>(_dbContext);
        }

        public Task Complete(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

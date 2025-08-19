using OMSX.Shared.Entities.Common;
using System.Linq.Expressions;
namespace OMSX.Shared.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : AuditEntityBase
    {
        Task<List<TEntity>> ListAsync(List<Expression<Func<TEntity, bool>>>? predicates = null);
        IQueryable<TEntity> Query();
        Task<TEntity> FirstAsync(Guid id);
        Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> CreateAsync(TEntity newItem);
        Task CreateBulkAsync(List<TEntity> data);
        Task<TEntity> UpdateAsync(TEntity newItem);
        Task<int> GetTotalRecords(Expression<Func<TEntity, bool>>? predicate = null);
        Task<int> Complete(CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, bool softDelete = true);
        void Delete<T>(T target) where T : EntityBase;
        Task<TEntity?> FirstOrDefaultAsync();
    }
}

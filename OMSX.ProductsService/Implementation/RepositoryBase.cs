
using Microsoft.EntityFrameworkCore;
using OMSX.Shared.Entities.Common;
using OMSX.Shared.Interfaces;
using System.Linq.Expressions;

namespace OMSX.ProductsService.Implementation
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : AuditEntityBase
    {
        private readonly DbContext _dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> CreateAsync(TEntity newItem)
        {
            if (newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem), "New item cannot be null");
            }
            await _dbContext.Set<TEntity>().AddAsync(newItem);
            return newItem;
        }

        public async Task CreateBulkAsync(List<TEntity> data)
        {
            if (data == null || !data.Any())
            {
                throw new ArgumentNullException(nameof(data), "Data cannot be null or empty");
            }
            await _dbContext.Set<TEntity>().AddRangeAsync(data);
        }

        public void Delete<T>(T target) where T : EntityBase
        {
            _dbContext.Remove<T>(target);
        }

        public async Task DeleteAsync(Guid id, bool softDelete = true)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
            if (softDelete)
            {
                if (entity != null)
                {
                    entity.Status = OMSX.Shared.Enums.StatusEnum.Deleted;
                    _dbContext.Update(entity);
                }
            }
            else
            {
                _dbContext.Set<TEntity>().Remove(entity!);
            }
        }

        public async Task<TEntity> FirstAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty", nameof(id));
            }
            return await _dbContext.Set<TEntity>().FirstAsync(x => x.Id == id);
        }

        public async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null");
            }
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> FirstOrDefaultAsync()
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalRecords(Expression<Func<TEntity, bool>>? predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Set<TEntity>().CountAsync();
            }
            return await _dbContext.Set<TEntity>().CountAsync(predicate);
        }

        public Task<List<TEntity>> ListAsync(List<Expression<Func<TEntity, bool>>>? predicates = null)
        {
            if (predicates == null || !predicates.Any())
            {
                return _dbContext.Set<TEntity>().ToListAsync();
            }
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }
            return query.ToListAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> UpdateAsync(TEntity newItem)
        {
            _dbContext.Update(newItem);
            var existingItem = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == newItem.Id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException($"Entity with ID {newItem.Id} not found.");
            }
            existingItem.UpdatedAt = DateTime.UtcNow;
            existingItem.UpdatedBy = newItem.UpdatedBy;
            _dbContext.Entry(existingItem).CurrentValues.SetValues(newItem);
            return existingItem;
        }
        public Task<int> Complete(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}

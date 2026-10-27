using CivicDesk.Domain.Common;

namespace CivicDesk.Repository.Interface;

public interface IGenericRepository<TEntity> 
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id);

    Task AddAsync(TEntity entity);

    void Remove(TEntity entity);
}
using CivicDesk.Domain.Common;
using CivicDesk.Repository.Context;
using CivicDesk.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CivicDesk.Repository.Implementation;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _entities;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _entities.AddAsync(entity);
    }

    public void Remove(TEntity entity)
    {
        _entities.Remove(entity);
    }
}
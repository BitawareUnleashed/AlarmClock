using Microsoft.EntityFrameworkCore;

namespace BlazorAlarmClock.Server.Models;

public class AlarmDataRepository<TEntity, TKey>
    : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
{
    private readonly DbContext dbContext;
    private readonly DbSet<TEntity> set;

    public AlarmDataRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
        set = this.dbContext.Set<TEntity>();
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> GetAll()
    {
        return set.AsNoTracking();
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        var entity = await set.FindAsync(id);
        if (entity == null)
        {
            return null;
        }

        dbContext.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    /// <inheritdoc/>
    public async Task CreateAsync(TEntity entity)
    {
        set.Add(entity);
        await dbContext.SaveChangesAsync();
        dbContext.Entry(entity).State = EntityState.Detached;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(TEntity entity)
    {
        set.Update(entity);
        await dbContext.SaveChangesAsync();
        dbContext.Entry(entity).State = EntityState.Detached;
    }

    /// <inheritdoc/>
    public Task DeleteAsync(TKey id)
    {
        var entity = new TEntity()
        {
            Id = id,
        };
        set.Remove(entity);
        return dbContext.SaveChangesAsync();
    }
}


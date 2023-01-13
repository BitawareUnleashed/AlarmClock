using BlazorAlarmClock.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAlarmClock.Server.Models;

public class AlarmDataRepository : IRepository<Alarm, int>
{
    private readonly DbContext dbContext;
    private readonly DbSet<Alarm> set;

    public AlarmDataRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
        set = this.dbContext.Set<Alarm>();
    }

    /// <inheritdoc/>
    public IQueryable<Alarm> GetAll()
    {
        return set.Include(e => e.AlarmDays).AsNoTracking();
        //return set.AsNoTracking();
    }

    /// <inheritdoc/>
    public async Task<Alarm?> GetByIdAsync(int id)
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
    public async Task CreateAsync(Alarm entity)
    {
        set.Add(entity);
        await dbContext.SaveChangesAsync();
        dbContext.Entry(entity).State = EntityState.Detached;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Alarm entity)
    {
        _ = DeleteAsync(entity.Id);
        await dbContext.SaveChangesAsync();

        _ = CreateAsync(entity);
        await dbContext.SaveChangesAsync();
        dbContext.Entry(entity).State = EntityState.Detached;
    }

    /// <inheritdoc/>
    public Task DeleteAsync(int id)
    {
        var entity = new Alarm()
        {
            Id = id,
        };
        set.Remove(entity);
        return dbContext.SaveChangesAsync();
    }

}


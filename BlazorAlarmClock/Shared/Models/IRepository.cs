namespace BlazorAlarmClock.Server.Models;

public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
{
    /// <summary>
    /// Gets all elements in our database.
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Gets an element in our database by his identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(TKey id);

    /// <summary>
    /// Creates new entity in our database.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task CreateAsync(TEntity entity);

    /// <summary>
    /// Updates an existing element in our database.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an element in our database.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task DeleteAsync(TKey id);
}


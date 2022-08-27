namespace QnA.Application.Data;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    #region Methods

    /// <summary>
    /// Get the entity entry
    /// </summary>
    /// <param name="id">Entity entry identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the entity entry
    /// </returns>
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);


    /// <summary>
    /// Get entity entries by identifiers
    /// </summary>
    /// <param name="ids">Entity entry identifiers</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the entity entries
    /// </returns>
    IQueryable<TEntity> GetByIds(IList<int> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entity entries
    /// </summary>
    /// <param name="func">Function to select entries</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the entity entries
    /// </returns>
    IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null);

    /// <summary>
    /// Get all entity entries
    /// </summary>
    /// <param name="func">Function to select entries</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the entity entries
    /// </returns>
    Task<IQueryable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Insert the entity entry
    /// </summary>
    /// <param name="entity">Entity entry</param>
    /// <param name="publishEvent">Whether to publish event notification</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// Insert entity entries
    /// </summary>
    /// <param name="entities">Entity entries</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task InsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update the entity entry
    /// </summary>
    /// <param name="entity">Entity entry</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Update entity entries
    /// </summary>
    /// <param name="entities">Entity entries</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task UpdateAsync(IList<TEntity> entities);

    /// <summary>
    /// Delete the entity entry
    /// </summary>
    /// <param name="entity">Entity entry</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Delete entity entries
    /// </summary>
    /// <param name="entities">Entity entries</param>
    /// <param name="publishEvent">Whether to publish event notification</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task DeleteAsync(IList<TEntity> entities);

    /// <summary>
    /// Delete entity entries by the passed predicate
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the number of deleted records
    /// </returns>
    Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// A task represents saving changes
    /// </summary>
    /// <returns></returns>
    Task<int> SaveAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Properties

    /// <summary>
    /// Gets a table
    /// </summary>
    DbSet<TEntity> Table { get; }

    #endregion
}

namespace QnA.Application.Data;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    #region Fields
    private readonly QnADbContext _qnaDbContext;
    #endregion

    #region ctor
    public BaseRepository(QnADbContext qnADbContext)
    {
        _qnaDbContext = qnADbContext;
    }
    #endregion

    public DbSet<TEntity> Table => _qnaDbContext.Set<TEntity>();

    #region Methods
    public Task DeleteAsync(TEntity entity)
    {
        Table.Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(IList<TEntity> entities)
    {
        Table.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var toDelete = Table.Where(predicate);
        int count = await toDelete.CountAsync(cancellationToken);
        Table.RemoveRange(toDelete);
        return count;
    }

    public IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null)
    {
        var data = Table.AsQueryable();
        if (func is not null)
            data = func(data);
        return data;
    }

    public async Task<IQueryable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null, CancellationToken cancellationToken = default)
    {
        var data = Table.AsQueryable().AsNoTracking();
        if (func is not null)
            data = await func(data);
        return data;
    }

    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Table.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }

    public  IQueryable<TEntity> GetByIds(IList<int> ids, CancellationToken cancellationToken = default)
    {
        return  Table.Where(e => ids.Contains(e.Id));
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Table.AddAsync(entity, cancellationToken);
    }

    public async Task InsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await Table.AddRangeAsync(entities, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await Task.FromResult(Table.Update(entity));
    }

    public Task UpdateAsync(IList<TEntity> entities)
    {
        Table.UpdateRange(entities);
        return Task.CompletedTask;
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await _qnaDbContext.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
